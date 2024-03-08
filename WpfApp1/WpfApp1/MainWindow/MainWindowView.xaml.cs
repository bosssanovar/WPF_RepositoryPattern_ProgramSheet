﻿using Entity.XX;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Windows;

namespace WpfApp1.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        #region Constants -------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Fields ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Properties ------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Events ----------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Constructor -----------------------------------------------------------------------------------

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model">MainWindowModelインスタンス</param>
        public MainWindowView(MainWindowModel model)
        {
            #region View Element

            #endregion

            #region ViewModel Element

            _model = model;

            Text = _model.Entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Text.Content,
                x =>
                {
                    var corrected = TextVO.CurrectValue(x);
                    _model.Entity.Value.Text = new(corrected);

                    _model.ForceNotify();

                    return _model.Entity.Value;
                },
                ReactivePropertyMode.DistinctUntilChanged)
                .AddTo(_disposable);

            Number = _model.Entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Number.Content,
                x =>
                {
                    var corrected = NumberVO.CurrectValue(x);
                    _model.Entity.Value.Number = new(corrected);

                    _model.ForceNotify();

                    return _model.Entity.Value;
                },
                ReactivePropertyMode.DistinctUntilChanged)
                .AddTo(_disposable);

            Bool = _model.Entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.Bool.Content,
                x =>
                {
                    var corrected = BoolVO.CurrectValue(x);
                    _model.Entity.Value.Bool = new(corrected);

                    _model.ForceNotify();

                    return _model.Entity.Value;
                },
                ReactivePropertyMode.DistinctUntilChanged)
                .AddTo(_disposable);

            SomeEnum = _model.Entity.ToReactivePropertySlimAsSynchronized(
                x => x.Value,
                x => x.SomeEnum.Content,
                x =>
                {
                    var corrected = SomeEnumVO.CurrectValue(x);
                    _model.Entity.Value.SomeEnum = new(corrected);

                    _model.ForceNotify();

                    return _model.Entity.Value;
                },
                ReactivePropertyMode.DistinctUntilChanged)
                .AddTo(_disposable);

            SpeakerOnOffs = _model.Details.ToReadOnlyReactiveCollection(x => new DetailViewModel(x))
                .AddTo(_disposable);

            InitCommand.Subscribe(async () =>
            {
                await Task.Delay(500);

                _model.Init();
            })
            .AddTo(_disposable);

            SaveCommand.Subscribe(async () =>
            {
                await Task.Delay(500);

                _model.Save();
            })
            .AddTo(_disposable);

            InitComboBoxItems();
            Bool.Subscribe(x =>
            {
                InitComboBoxItems();
            })
            .AddTo(_disposable);

            #endregion

            InitializeComponent();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods ---------------------------------------------------------------------------------------

        #region Methods - public ------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - internal ----------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - protected ---------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - private -----------------------------------------------------------------------------

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _disposable.Dispose();
        }

        #endregion --------------------------------------------------------------------------------------------

        #region Methods - override ----------------------------------------------------------------------------

        /// <inheritdoc/>
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            UpdateEntity();
        }

        #endregion --------------------------------------------------------------------------------------------

        #endregion --------------------------------------------------------------------------------------------

    }
}