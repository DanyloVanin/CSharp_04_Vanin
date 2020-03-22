using System.Collections.Generic;
using CShar_Vanin_04.Models;

namespace CShar_Vanin_04.Tools.DataStorage
{
    internal interface IDataStorage
    {
        void AddPerson(Person person);
        void DeletePerson(Person person);
        void EditPerson(Person personToChange,Person editedPerson);
        void SaveChanges();
        List<Person> PersonsList { get; }
    }
}