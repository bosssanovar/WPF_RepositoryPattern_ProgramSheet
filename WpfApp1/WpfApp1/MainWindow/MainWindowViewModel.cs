using Entity.XX;
using Reactive.Bindings;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reactive.Disposables;

namespace WpfApp1.MainWindow
{
    public partial class MainWindowView : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // イベント 'MainWindow.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'MainWindow.PropertyChanged' は使用されていません

        private readonly MainWindowModel _model;

        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public bool IsAutoSave
        {
            get { return _model.IsAutoSave; }
            set
            {
                _model.IsAutoSave = value;
            }
        }

        public ReactivePropertySlim<string> Text { get; }
        public ReactivePropertySlim<int> Number { get; }
        public ReactivePropertySlim<bool> Bool { get; }
        public ReactivePropertySlim<SomeEnum> SomeEnum { get; }

        public ReadOnlyReactiveCollection<DetailViewModel> DataGridSource { get; }

        public AsyncReactiveCommand InitCommand { get; } = new AsyncReactiveCommand();
        public AsyncReactiveCommand SaveCommand { get; } = new AsyncReactiveCommand();

        public ReactivePropertySlim<List<ComboBoxItemDisplayValue<SomeEnum>>> ComboBoxItems { get; private set; } =
            new ReactivePropertySlim<List<ComboBoxItemDisplayValue<SomeEnum>>>();

        private void InitComboBoxItems()
        {
#pragma warning disable IDE0028 // コレクションの初期化を簡略化します
            var comboBoxItemList = new List<ComboBoxItemDisplayValue<SomeEnum>>();
#pragma warning restore IDE0028 // コレクションの初期化を簡略化します
            comboBoxItemList.Add(new ComboBoxItemDisplayValue<SomeEnum>(Entity.XX.SomeEnum.Dog, Entity.XX.SomeEnum.Dog.GetText()));
            if (Bool.Value)
            {
                comboBoxItemList.Add(new ComboBoxItemDisplayValue<SomeEnum>(Entity.XX.SomeEnum.Cat, Entity.XX.SomeEnum.Cat.GetText()));
            }
            comboBoxItemList.Add(new ComboBoxItemDisplayValue<SomeEnum>(Entity.XX.SomeEnum.Elephant, Entity.XX.SomeEnum.Elephant.GetText()));
            comboBoxItemList.Add(new ComboBoxItemDisplayValue<SomeEnum>(Entity.XX.SomeEnum.Pig, Entity.XX.SomeEnum.Pig.GetText()));

            ComboBoxItems.Value = new List<ComboBoxItemDisplayValue<SomeEnum>>(comboBoxItemList);
        }

        private void UpdateEntity()
        {
            _model.LoadEntity();
        }
    }
}
