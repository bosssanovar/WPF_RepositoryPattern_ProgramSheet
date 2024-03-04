using Entity.XX;
using Microsoft.Extensions.DependencyInjection;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using Repository;
using RepositoryMonitor;
using System.Diagnostics;
using System.Windows;
using Usecase;
using WpfApp1.MainWindow;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        private readonly RepositoryMonitorView _repositoryMonitorView;

        public MainWindowView(Model model, RepositoryMonitorView repositoryMonitorView)
        {
            _model = model;

            _repositoryMonitorView = repositoryMonitorView;

            Text = _model.Entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Text.Content,
                x =>
                {
                    Debug.WriteLine("Text ConvertBack");

                    var currected = TextVO.CurrectValue(x);
                    _model.Entity.Value.Text = new(currected);

                    _model.ForceNotify();

                    return _model.Entity.Value;
                });

            Number = _model.Entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Number.Content,
                x =>
                {
                    Debug.WriteLine("Number ConvertBack");

                    var currected = NumberVO.CurrectValue(x);
                    _model.Entity.Value.Number = new(currected);

                    _model.ForceNotify();

                    return _model.Entity.Value;
                });

            Bool = _model.Entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Bool.Content,
                x =>
                {
                    Debug.WriteLine("Bool ConvertBack");

                    var currected = BoolVO.CurrectValue(x);
                    _model.Entity.Value.Bool = new(currected);

                    _model.ForceNotify();

                    return _model.Entity.Value;
                });

            SomeEnum = _model.Entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.SomeEnum.Content,
                x =>
                {
                    Debug.WriteLine("SomeEnum ConvertBack");

                    var currected = SomeEnumVO.CurrectValue(x);
                    _model.Entity.Value.SomeEnum = new(currected);

                    _model.ForceNotify();

                    return _model.Entity.Value;
                });

            InitCommand = new AsyncReactiveCommand();
            InitCommand.Subscribe(async () =>
            {
                await Task.Delay(500);

                _model.Init();
            });

            SaveCommand = new AsyncReactiveCommand();
            SaveCommand.Subscribe(async () =>
            {
                await Task.Delay(500);

                _model.Save();
            });

            ComboBoxItems = new ReactivePropertySlim<List<ComboBoxItemDisplayValue<SomeEnum>>>();
            InitComboBoxItems();

            InitMediator();

            InitializeComponent();
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            _repositoryMonitorView.Owner = this;
            _repositoryMonitorView.Show();
        }
    }
}