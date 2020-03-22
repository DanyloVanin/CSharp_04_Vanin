using System.Windows;
using CShar_Vanin_04.Tools;
using CShar_Vanin_04.Tools.DataStorage;
using CShar_Vanin_04.Tools.Managers;
using CShar_Vanin_04.Tools.Navigation;

namespace CShar_Vanin_04.ViewModels
{
    internal class MainWindowViewModel : BaseViewModel, ILoaderOwner, IContentOwner
    {
        #region Fields
        private Visibility _loaderVisibility = Visibility.Hidden;
        private bool _isControlEnabled = true;
        private INavigatable _content;
        #endregion

        #region Properties
        public INavigatable Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }
        public Visibility LoaderVisibility
        {
            get => _loaderVisibility;
            set
            {
                _loaderVisibility = value;
                OnPropertyChanged();
            }
        }
        public bool IsControlEnabled
        {
            get => _isControlEnabled;
            set
            {
                _isControlEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        internal MainWindowViewModel()
        {
            StationManager.Initialize(new SerializedDataStorage());
            LoaderManager.Instance.Initialize(this);
            NavigationManager.Instance.Initialize(new PersonListNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.PersonGrid);
        }
    }
}