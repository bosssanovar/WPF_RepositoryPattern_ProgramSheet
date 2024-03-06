﻿using Entity.XX;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.ObjectModel;
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

        public ObservableCollection<DetailModel> Details { get; } = new ObservableCollection<DetailModel>();

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
                    _saveLoadUsecase.Save(x);
                }
            });
        }

        public void LoadEntity()
        {
            Entity.Value = _saveLoadUsecase.Load();
            InitDetails();
        }

        private void InitDetails()
        {
            ClearDetails();
            foreach (var speakerOnOffEntity in Entity.Value.SpeakerOnOff.Details)
            {
                var detail = new DetailModel(speakerOnOffEntity);
                detail.ContentChanged += Detail_ContentChanged;
                Details.Add(detail);
            }
        }

        private void ClearDetails()
        {
            foreach(var detail in Details)
            {
                detail.ContentChanged -= Detail_ContentChanged;
            }

            Details.Clear();
        }

        private void Detail_ContentChanged()
        {
            if (IsAutoSave)
            {
                _saveLoadUsecase.Save(Entity.Value);
            }
        }

        internal void Init()
        {
            _initUsecase.Init();
            LoadEntity();
        }

        internal void Save()
        {
            _saveLoadUsecase.Save(Entity.Value);
        }

        internal void ForceNotify()
        {
            Entity.ForceNotify();
        }
    }
}
