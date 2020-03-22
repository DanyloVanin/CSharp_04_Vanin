using CShar_Vanin_04.Models;
using CShar_Vanin_04.Tools;
using CShar_Vanin_04.Tools.Managers;
using CShar_Vanin_04.Tools.MVVM;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CShar_Vanin_04.Tools.Navigation;

namespace CShar_Vanin_04.ViewModels
{
    class PersonGridViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<Person> _persons;
        private Person _selectedPerson;

        #region Commands
        private RelayCommand<object> _editPerson;
        private RelayCommand<object> _addPerson;
        private RelayCommand<object> _deletePerson;
        private RelayCommand<object> _saveGrid;

        private RelayCommand<object> _sortByName;
        private RelayCommand<object> _sortBySurname;
        private RelayCommand<object> _sortByEmail;
        private RelayCommand<object> _sortByBirthday;
        private RelayCommand<object> _sortByWesternZodiac;
        private RelayCommand<object> _sortByChineseZodiac;
        private RelayCommand<object> _sortByIsAdult;
        private RelayCommand<object> _sortByIsBirthday;
        #endregion

        #endregion

        #region Properties
        public Person SelectedPerson
        {
            get => _selectedPerson;
            set => _selectedPerson = value;
        }
        public ObservableCollection<Person> Persons
        {
            get => _persons;
            private set
            {
                _persons = value;
                OnPropertyChanged();
            }
        }

        #region Commands
        public RelayCommand<object> SaveGridCommand
        {
            get
            {
                return _saveGrid ??= new RelayCommand<object>(
                    SaveGridImplementation);
            }
        }
        public RelayCommand<object> AddPersonCommand
        {
            get
            {
                return _addPerson ??= new RelayCommand<object>(
                    AddPersonImplementation);
            }
        }
        public RelayCommand<object> EditPersonCommand
        {
            get
            {
                return _editPerson ??= new RelayCommand<object>(
                    EditPersonImplementation, o => _selectedPerson != null);
            }
        }
        public RelayCommand<object> DeletePersonCommand
        {
            get
            {
                return _deletePerson ??= new RelayCommand<object>(
                    DeletePersonImplementation, o => _selectedPerson != null);
            }
        }
        #region SortCommands
        public RelayCommand<object> SortNameCommand
        {
            get
            {
                return _sortByName ??= new RelayCommand<object>(
                   o => SortControlMethod(1));
            }
        }
        public RelayCommand<object> SortSurnameCommand
        {
            get
            {
                return _sortBySurname ??= new RelayCommand<object>(
                    o => SortControlMethod(2));
            }
        }
        public RelayCommand<object> SortEmailCommand
        {
            get
            {
                return _sortByEmail ??= new RelayCommand<object>(
                    o => SortControlMethod(3));
            }
        }
        public RelayCommand<object> SortBirthdayCommand
        {
            get
            {
                return _sortByBirthday ??= new RelayCommand<object>(
                    o => SortControlMethod(4));
            }
        }
        public RelayCommand<object> SortWesternZodiacCommand
        {
            get
            {
                return _sortByWesternZodiac ??= new RelayCommand<object>(
                    o => SortControlMethod(5));
            }
        }
        public RelayCommand<object> SortChineseZodiacCommand
        {
            get
            {
                return _sortByChineseZodiac ??= new RelayCommand<object>(
                    o => SortControlMethod(6));
            }
        }
        public RelayCommand<object> SortIsAdultCommand
        {
            get
            {
                return _sortByIsAdult ??= new RelayCommand<object>(
                    o => SortControlMethod(7));
            }
        }
        public RelayCommand<object> SortIsBirthdayCommand
        {
            get
            {
                return _sortByIsBirthday ??= new RelayCommand<object>(
                    o => SortControlMethod(8));
            }
        }
        #endregion
        #endregion

        #endregion

        #region Constructor
        internal PersonGridViewModel()
        {
            LoaderManager.Instance.ShowLoader();
            Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
            StationManager.DataStorage.PersonsList.CollectionChanged += OnCollectionChanged;
            LoaderManager.Instance.HideLoader();
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            switch (eventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
                    break;
            }
        }

        #endregion

        #region Functions
        private async void SortControlMethod(int i)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                var sortedPeople = i switch
                {
                    1 => (from p in _persons orderby p.Name select p),
                    2 => (from p in _persons orderby p.Surname select p),
                    3 => (from p in _persons orderby p.Email select p),
                    4 => (from p in _persons orderby p.BirthDate select p),
                    5 => (from p in _persons orderby p.WesternZodiac select p),
                    6 => (from p in _persons orderby p.ChineseZodiac select p),
                    7 => (from p in _persons orderby p.IsAdult select p),
                    _ => (from p in _persons orderby p.IsBirthday select p)
                };
                Persons = new ObservableCollection<Person>(sortedPeople);
                Thread.Sleep(500);
            });
            LoaderManager.Instance.HideLoader();
        }
        private async void SaveGridImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                StationManager.DataStorage.SaveChanges();
                Thread.Sleep(500);
            });
            LoaderManager.Instance.HideLoader();
        }
        private void AddPersonImplementation(object obj)
        {
            StationManager.SelectedUser = null;
            NavigationManager.Instance.Navigate(ViewType.AddEdit);
        }
        private void EditPersonImplementation(object obj)
        {
            StationManager.SelectedUser = _selectedPerson;
            NavigationManager.Instance.Navigate(ViewType.AddEdit);
        }
        private async void DeletePersonImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                if (MessageBox.Show($"Are you sure about deleting {_selectedPerson.Name} {_selectedPerson.Surname}?",
                    "Delete?",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
                StationManager.DataStorage.DeletePerson(_selectedPerson);
                _selectedPerson = null;
                StationManager.SelectedUser = null;
                Thread.Sleep(500);
            });
            LoaderManager.Instance.HideLoader();
        }

        #endregion
    }
}
