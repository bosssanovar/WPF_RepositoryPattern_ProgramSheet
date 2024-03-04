using Entity.XX;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usecase;

namespace WpfApp1.Setup
{
    public class SetupModel
    {
        internal ReactivePropertySlim<XXEntity> Entity { get; } = new ReactivePropertySlim<XXEntity>();

        private readonly SaveLoadUsecase _saveLoadUsecase;

        public SetupModel(SaveLoadUsecase saveLoadUsecase)
        {
            _saveLoadUsecase = saveLoadUsecase;
            LoadEntity();
        }

        private void LoadEntity()
        {
            Entity.Value = _saveLoadUsecase.Load();
        }

        internal void ForceNotify()
        {
            Entity.ForceNotify();
        }

        internal void Save()
        {
            _saveLoadUsecase.Save(Entity.Value);
        }
    }
}
