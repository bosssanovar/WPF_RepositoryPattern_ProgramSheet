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
        public ReactivePropertySlim<SpeakerOnOffDetailEntity> Entity { get; }

        public event Action? ContentChanged;

        public DetailModel(SpeakerOnOffDetailEntity entity)
        {
            Entity = new ReactivePropertySlim<SpeakerOnOffDetailEntity>(entity);

            Entity.Subscribe(x =>
            {
                ContentChanged?.Invoke();
            });
        }

        internal void ForceNotify()
        {
            Entity.ForceNotify();
        }
    }
}
