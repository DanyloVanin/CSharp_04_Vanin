using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CShar_Vanin_04.Models;
using CShar_Vanin_04.Tools.Managers;

namespace CShar_Vanin_04.Tools.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private List<Person> _persons;

        internal SerializedDataStorage()
        {
            try
            {
                _persons = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _persons = new List<Person>();
                FillPersonsRandomly();
                SaveChanges();
            }
        }

        private void FillPersonsRandomly()
        {
            DateTime minDate = new DateTime(1900, 1, 1);
            DateTime maxDate = DateTime.Today;
            int daySpan = maxDate.Subtract(minDate).Days;
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                AddPerson(new Person($"Name{rand.Next(1, 60)}",
                    $"Surname{rand.Next(1, 60)}",
                    $"mail{rand.Next(1, 60)}@edu.ua",
                    minDate.AddDays(rand.Next(1, daySpan))));
            }
        }

        public void AddPerson(Person person)
        {
            _persons.Add(person);
            SaveChanges();
        }

        public void DeletePerson(Person person)
        {
            _persons.Remove(person);
            SaveChanges();
        }

        public void EditPerson(Person personToChange, Person editedPerson)
        {
            _persons[_persons.IndexOf(personToChange)] = editedPerson;
        }


        public List<Person> PersonsList
        {
            get => _persons.ToList();
            set => _persons = value;
        }

        public void SaveChanges()
        {
            SerializationManager.Serialize(_persons, FileFolderHelper.StorageFilePath);
        }
    }
}