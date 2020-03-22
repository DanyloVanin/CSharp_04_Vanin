using System;
using System.ComponentModel.DataAnnotations;
using CShar_Vanin_04.Exceptions;

namespace CShar_Vanin_04.Models
{
    [Serializable]
    public class Person
    {
        #region Fields

        private string _name;
        private string _surname;
        private string _email;
        private DateTime _birthDate;

        private readonly bool _isAdult;
        private readonly bool _isBirthday;
        private readonly string _westernZodiac;
        private readonly string _chineseZodiac;

        #endregion

        #region Properties

        public string Name {
            get => _name;
            private set => _name = value;
        }
        public string Surname {
            get => _surname;
            private set => _surname = value;
        }
        public string Email
        {
            get => _email;
            private set
            {
                if (new EmailAddressAttribute().IsValid(value))
                    _email = value;
                else throw new InvalidEmailException("Wrong email address!");
            } 
        }
        public DateTime BirthDate
        {
            get => _birthDate;
            private set => _birthDate = value;
        }
        public bool IsAdult
        {
            get => _isAdult;
        }
        public bool IsBirthday
        {
            get => _isBirthday;
        }
        public string WesternZodiac
        {
            get => _westernZodiac;
        }
        public string ChineseZodiac
        {
            get => _chineseZodiac;
        }

        #endregion

        #region Constructors

        internal Person(string name, string surname, string email, DateTime birthDate) {
            Name = name;
            Surname = surname;
            Email = email;
            BirthDate = birthDate;
            _isBirthday = CheckIfBirthday();
            _isAdult = (CountAge() >= 18);
            _westernZodiac = FindWesternZodiac();
            _chineseZodiac = FindChineseZodiac();
        }
        private Person(string name, string surname, string email): this(name, surname,email,DateTime.Today) { }
        private Person(string name, string surname, DateTime birthDate) : this(name, surname, "user@gmail.com", birthDate) { }
        
        #endregion

        private int CountAge() {
            DateTime today=DateTime.Today;
            int years = today.Year - BirthDate.Year;
            if ((today.Month < BirthDate.Month) || (today.Month == BirthDate.Month && today.Day < BirthDate.Day)) years--;
            if(years<0) throw new BirthDateInFutureException("InvalidBirthDate - Date is too far in the future");
            if (years > 135) throw new PersonTooOldException("InvalidBirthDate - Date is too far in the past");
            return years;
        }
        private bool CheckIfBirthday()
        {
            return (DateTime.Today.Month == _birthDate.Month && DateTime.Today.Day == _birthDate.Day);
        } 
        private string FindWesternZodiac()
        {
            int day = _birthDate.Day;
            return _birthDate.Month switch
            {
                1 => (day < 20 ? "Capricorn" : "Aquarius"),
                2 => (day < 19 ? "Aquarius" : "Pisces"),
                3 => (day < 21 ? "Pisces" : "Aries"),
                4 => (day < 20 ? "Aries" : "Taurus"),
                5 => (day < 21 ? "Taurus" : "Gemini"),
                6 => (day < 21 ? "Gemini" : "Cancer"),
                7 => (day < 23 ? "Cancer" : "Leo"),
                8 => (day < 23 ? "Leo" : "Virgo"),
                9 => (day < 23 ? "Virgo" : "Libra"),
                10 => (day < 23 ? "Libra" : "Scorpio"),
                11 => (day < 22 ? "Scorpio" : "Sagittarius"),
                _ => (day < 22 ? "Sagittarius" : "Capricorn")
            };
        }
        private string FindChineseZodiac()
        {
            return (_birthDate.Year % 12) switch
            {
                0 => "Monkey",
                1 => "Rooster",
                2 => "Dog",
                3 => "Pig",
                4 => "Rat",
                5 => "Ox",
                6 => "Tiger",
                7 => "Rabbit",
                8 => "Dragon",
                9 => "Snake",
                10 => "Horse",
                _ => "Goat"
            };
        }
        
    }
}
