using System;
using System.Collections.Generic;
using System.Text;

namespace PlayersAndMonsters.Validators
{
    public static class Validator
    {
        public static void ValidateStringIfNullOrEmpty(string value,string message)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException(message);
            }
        }

        public static void ValidateIntIfLessThanZerro(int value,string message)
        {
            if (value < 0)
            {
                throw new ArgumentException(message);
            }
        }
    }
}
