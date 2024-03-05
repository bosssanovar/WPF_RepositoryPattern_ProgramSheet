using Entity.XX;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.MainWindow
{
    public class DetailModel
    {
        private bool _isUpdating = false;

        public SpeakerOnOffDetailEntity Entity { get; private set; }

        public List<ReactivePropertySlim<SpeakerOnOffVO>> SpeakerOnOffs { get; } = [];

        public event Action? ContentChanged;

        public DetailModel(SpeakerOnOffDetailEntity entity)
        {
            Entity = entity;

            for (int i = 0; i < Entity.SpeakerOnOffDetail.Count; i++)
            {
                SpeakerOnOffVO? sp = Entity.SpeakerOnOffDetail[i];
                var reactiveSp = new ReactivePropertySlim<SpeakerOnOffVO>(sp);
                int index = i;
                reactiveSp.Subscribe(x => 
                {
                    if (!_isUpdating)
                    {
                        Entity.SpeakerOnOffDetail[index] = x;
                        ContentChanged?.Invoke();
                    }
                });

                SpeakerOnOffs.Add(reactiveSp);
            }
        }

        public void Update(SpeakerOnOffDetailEntity entity)
        {
            _isUpdating = true;

            Entity = entity;

            for(int i = 0; i < SpeakerOnOffs.Count; i++)
            {
                SpeakerOnOffs[i].Value = entity.SpeakerOnOffDetail[i];
            }

            _isUpdating = false;
        }
    }
}
