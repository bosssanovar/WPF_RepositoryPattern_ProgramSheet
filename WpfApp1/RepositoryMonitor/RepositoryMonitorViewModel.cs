using Entity;
using Entity.XX;
using Reactive.Bindings;
using Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryMonitor
{
    public partial class RepositoryMonitorView : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // イベント 'RepositoryMonitorView.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'RepositoryMonitorView.PropertyChanged' は使用されていません

        readonly IXXRepository _repository;
        XXEntity? _oldEntity;

        public ReactiveCollection<Item> Items { get; } = [];

        public AsyncReactiveCommand ClearCommand { get; } = new AsyncReactiveCommand();

        private void DetectDifference()
        {
            _oldEntity ??= _repository.Load();

            var newEntity = _repository.Load();

            var oldProperties = GetEntityProperties(_oldEntity);
            var newProperties = GetEntityProperties(newEntity);

            foreach (var name in oldProperties.Keys)
            {
                if (!newProperties.ContainsKey(name)) continue;

                if (oldProperties[name] != newProperties[name])
                {
                    Items.Insert(0, new Item(name, newProperties[name], oldProperties[name]));
                }
            }

            _oldEntity = newEntity;
        }

        private static Dictionary<string, string> GetEntityProperties(XXEntity entity)
        {
            var ret = new Dictionary<string, string>();

            // Entityの型を取得
            Type entityType = entity.GetType();

            // Entityのプロパティの一覧を取得
            PropertyInfo[] entityPropertyInfos = entityType.GetProperties();

            // Entityのプロパティ一覧を列挙
            foreach (PropertyInfo entityPropertyInfo in entityPropertyInfos)
            {
                // Entity直下のオブジェクトのプロパティ名を取得
                string entityPropertyName = entityPropertyInfo.Name;

                // Entity直下のオブジェクトを取得
                var entityPropertyValue = entityPropertyInfo.GetValue(entity);

                if (entityPropertyValue is ISettingInfos)
                {
                    //  Entity直下のオブジェクトの型を取得
                    Type voPropertyType = entityPropertyValue.GetType();

                    // Entity直下のオブジェクトのSettingInfosプロパティ情報を取得
                    var voPropertyInfo = voPropertyType.GetProperty("SettingInfos");

                    if (voPropertyInfo != null)
                    {
                        // Entity直下のオブジェクトのContentプロパティ値を取得
                        var voPropertyValue = voPropertyInfo.GetValue(entityPropertyValue);

                        if (voPropertyValue is List<(string Name, string Value)> infos)
                        {
                            foreach (var info in infos)
                            {
                                ret[info.Name] = info.Value;
                            }
                        }
                    }
                }
            }

            return ret;
        }
    }
}
