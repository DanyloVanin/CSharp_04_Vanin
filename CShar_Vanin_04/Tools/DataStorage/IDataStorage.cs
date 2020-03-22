using System.Collections.ObjectModel;
using CShar_Vanin_04.Models;

namespace CShar_Vanin_04.Tools.DataStorage
{
    internal interface IDataStorage
    {
        void AddPerson(Person person);
        void DeletePerson(Person person);
        void EditPerson(Person personToChange,Person editedPerson);
        void SaveChanges();
        ObservableCollection<Person> PersonsList { get; }
    }
}