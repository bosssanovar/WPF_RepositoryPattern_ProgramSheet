using Entity.XX;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.MainWindow
{
    public class DetailViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private DetailModel _model;

        private CompositeDisposable _disposable = new CompositeDisposable();

        public List<ReactivePropertySlim<bool>> SpeakerOnOff { get; set; } = [];

        public DetailViewModel(DetailModel model)
        {
            _model = model;

            var count = _model.Entity.Value.SpeakerOnOffDetail.Count;
            for (int i = 0; i < count; i++)
            {
                int index = i;
                var sp = _model.Entity.ToReactivePropertySlimAsSynchronized(
                    x => x.Value,
                    x => x.SpeakerOnOffDetail[index].Content,
                    x =>
                    {
                        var corrected = SpeakerOnOffVO.CurrectValue(x);
                        _model.Entity.Value.SpeakerOnOffDetail[index] = new(corrected);

                        _model.ForceNotify();

                        return _model.Entity.Value;
                    },
                    ReactivePropertyMode.DistinctUntilChanged)
                    .AddTo(_disposable);

                SpeakerOnOff.Add(sp);
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}
