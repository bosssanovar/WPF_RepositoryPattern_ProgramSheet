using Entity.XX;
using Reactive.Bindings;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace WpfApp1.MainWindow
{
    public partial class MainWindowView : INotifyPropertyChanged
    {
#pragma warning disable CS0067 // イベント 'MainWindow.PropertyChanged' は使用されていません
        public event PropertyChangedEventHandler? PropertyChanged;
#pragma warning restore CS0067 // イベント 'MainWindow.PropertyChanged' は使用されていません

        private readonly MainWindowModel _model;

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

        public ObservableCollection<DetailViewModel> DataGridSource { get; } = new ObservableCollection<DetailViewModel>();

        public AsyncReactiveCommand InitCommand { get; }
        public AsyncReactiveCommand SaveCommand { get; }

        public ReactivePropertySlim<List<ComboBoxItemDisplayValue<SomeEnum>>> ComboBoxItems { get; private set; }

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

        private void InitDataGridSource()
        {
            DataGridSource.Clear();
            foreach (var detailModel in _model.Details)
            {
                DataGridSource.Add(new DetailViewModel(detailModel));
            }
        }
    }
}
