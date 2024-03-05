using Entity.XX;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Windows;
using Usecase;

namespace WpfApp1.MainWindow
{
    public class MainWindowModel
    {
        public bool IsAutoSave { get; set; }

        public ReactivePropertySlim<XXEntity> Entity { get; } = new ReactivePropertySlim<XXEntity>();

        public List<DetailModel> Details { get; } = new List<DetailModel>();

        private readonly SaveLoadUsecase _saveLoadUsecase;

        private readonly InitUsecase _initUsecase;

        public MainWindowModel(SaveLoadUsecase saveLoadUsecase, InitUsecase initUsecase)
        {
            _saveLoadUsecase = saveLoadUsecase;
            _initUsecase = initUsecase;

            LoadEntity();
            Entity.Subscribe(x =>
            {
                if (IsAutoSave)
                {
                    Debug.WriteLine("Auto Save");

                    _saveLoadUsecase.Save(x);
                }
            });
        }

        public void LoadEntity()
        {
            Debug.WriteLine("LoadEntity");

            Entity.Value = _saveLoadUsecase.Load();
            InitDetails();
        }

        private void InitDetails()
        {
            Details.Clear();
            foreach (var speakerOnOffEntity in Entity.Value.SpeakerOnOff)
            {
                var detail = new DetailModel(speakerOnOffEntity);
                detail.ContentChanged += Detail_ContentChanged;
                Details.Add(detail);
            }
        }

        private void Detail_ContentChanged()
        {
            if (IsAutoSave)
            {
                Debug.WriteLine("Auto Save");

                _saveLoadUsecase.Save(Entity.Value);
            }
        }

        internal void Init()
        {
            Debug.WriteLine("Init");

            _initUsecase.Init();
            LoadEntity();
        }

        internal void Save()
        {
            Debug.WriteLine("Save");

            _saveLoadUsecase.Save(Entity.Value);
        }

        internal void ForceNotify()
        {
            Entity.ForceNotify();
        }
    }
}
