using System;

namespace CShar_Vanin_04.Exceptions
{
    internal class BirthDateInFutureException: ArgumentException
    {
        public BirthDateInFutureException(string message) : base(message) { }
    }
}
