using System;
using System.Windows;
using CShar_Vanin_04.Models;
using CShar_Vanin_04.Tools;
using CShar_Vanin_04.Tools.Managers;
using CShar_Vanin_04.Tools.MVVM;

namespace CShar_Vanin_04.ViewModels
{
    class AddEditViewModel: BaseViewModel
    {
        #region Fields
        private readonly Person _person;
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
        public Action CloseAction { get; set; }
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

        internal AddEditViewModel(Person person)
        {
            _person = person;
            _name = person.Name;
            _surname = person.Surname;
            _email = person.Email;
            _birthDate = person.BirthDate;
        }

        internal AddEditViewModel()
        {
        }

        #endregion

        #region Functions
        private void SubmitImplementation(object obj)
        {
            try {
                if (_person != null)
                {
                    if (MessageBox.Show("Are you sure you want to edit Person?", "Edit?",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes) return;
                    StationManager.DataStorage.EditPerson(_person, new Person(Name, Surname, Email, Date));
                    CloseAction();
                }
                else
                {
                    StationManager.DataStorage.AddPerson(new Person(Name, Surname, Email, Date));
                    MessageBox.Show("You have added new Person!");
                    CloseAction();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void CancelImplementation(object obj)
        {
            CloseAction();
        }

        private bool CanExecuteCommand()
        {
            return !string.IsNullOrWhiteSpace(_name) && !string.IsNullOrWhiteSpace(_surname) && !string.IsNullOrWhiteSpace(_email);
        }
        #endregion
    }
}
