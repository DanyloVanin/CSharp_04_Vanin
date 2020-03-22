using System;

namespace CShar_Vanin_04.Exceptions
{
    internal class PersonTooOldException: ArgumentException
    {
        public PersonTooOldException(string message) : base(message) { }
    }
}
