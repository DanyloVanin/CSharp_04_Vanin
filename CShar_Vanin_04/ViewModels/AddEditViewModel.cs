using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using CShar_Vanin_04.Models;
using CShar_Vanin_04.Tools;
using CShar_Vanin_04.Tools.Managers;
using CShar_Vanin_04.Tools.MVVM;
using CShar_Vanin_04.Tools.Navigation;

namespace CShar_Vanin_04.ViewModels
{
    class AddEditViewModel: BaseViewModel
    {
        #region Fields
        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDate = DateTime.Today;
        #region Commands
        private RelayCommand<object> _submitCommand;
        private RelayCommand<object> _cancelCommand;
        #endregion
        #endregion

        #region Properties

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }
        #region Commands
        public RelayCommand<object> SubmitCommand
        {
            get
            {
                return _submitCommand ??= new RelayCommand<object>(
                    SubmitImplementation, o => CanExecuteCommand());
            }
        }

        public RelayCommand<object> CancelCommand
        {
            get
            {
                return _cancelCommand ??= new RelayCommand<object>(
                    CancelImplementation);
            }
        }


        #endregion

        #endregion

        #region Constructors
        
        internal AddEditViewModel()
        {
            if (StationManager.SelectedUser == null) return;
            _name = StationManager.SelectedUser.Name;
            _surname = StationManager.SelectedUser.Surname;
            _email = StationManager.SelectedUser.Email;
            _birthDate = StationManager.SelectedUser.BirthDate;
        }

        #endregion

        #region Functions
        private async void SubmitImplementation(object obj)
        {
            if (StationManager.SelectedUser != null)
            {
                if (MessageBox.Show("Are you sure you want to edit Person?", "Edit?",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
                LoaderManager.Instance.ShowLoader();
                   
                await Task.Run(() =>
                {
                    try
                    {
                        StationManager.DataStorage.EditPerson(StationManager.SelectedUser,
                            new Person(Name, Surname, Email, Date));
                        Thread.Sleep(500);
                    } catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                });
                LoaderManager.Instance.HideLoader();
                NavigationManager.Instance.Navigate(ViewType.PersonGrid);
            }
            else
            {
                LoaderManager.Instance.ShowLoader();
                await Task.Run(() =>
                {
                    try { 
                        StationManager.DataStorage.AddPerson(new Person(Name, Surname, Email, Date));
                        Thread.Sleep(500);
                        MessageBox.Show("You have added new Person!");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                });
                LoaderManager.Instance.HideLoader();
                NavigationManager.Instance.Navigate(ViewType.PersonGrid);
            }
        }


        private void CancelImplementation(object obj)
        {
            NavigationManager.Instance.Navigate(ViewType.PersonGrid);
        }

        private bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(_name) && !string.IsNullOrWhiteSpace(_surname) && !string.IsNullOrWhiteSpace(_email);
        }
        #endregion
    }
}
