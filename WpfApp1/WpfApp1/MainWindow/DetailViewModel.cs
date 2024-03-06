using Entity.XX;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.MainWindow
{
    public class DetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private DetailModel _model;

        public List<ReactivePropertySlim<bool>> SpeakerOnOff { get; set; } = [];

        public DetailViewModel(DetailModel model)
        {
            _model = model;

            foreach(var seakerOnOff in _model.SpeakerOnOffs)
            {
                ReactivePropertySlim<bool> sp = seakerOnOff.ToReactivePropertySlimAsSynchronized(
                    x => x.Value,
                    x => x.Content,
                    x =>
                    {
                        return new SpeakerOnOffVO(x);
                    });
                SpeakerOnOff.Add(sp);
            }
        }
    }
}
