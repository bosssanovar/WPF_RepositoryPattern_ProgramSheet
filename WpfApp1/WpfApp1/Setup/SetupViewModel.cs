using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Setup
{
    public partial class SetupView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly SetupModel _model;

        public ReactivePropertySlim<int> SpeakerCount { get; }

        public AsyncReactiveCommand SaveCommand { get; } = new AsyncReactiveCommand();
    }
}
