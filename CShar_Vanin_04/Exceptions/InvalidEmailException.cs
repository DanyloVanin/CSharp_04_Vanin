using System;

namespace CShar_Vanin_04.Exceptions
{
    internal class InvalidEmailException: ArgumentException
    {
        public InvalidEmailException(string message) : base(message) { }
    }
}
