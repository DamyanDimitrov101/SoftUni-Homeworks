using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DateModifier
{
    public class DateModifier
    {
        public DateModifier()
        {
        
        }

        

        public DateTime DateA { get; set; }
        public DateTime DateB { get; set; }

        public long GetDiff(string a, string b)
        {
            int years, mounts, days;
            ReturnDateParts(a, out years, out mounts, out days);

            DateA = new DateTime(years, mounts, days);

            int years1, mounts1, days1;
            ReturnDateParts(b, out years1, out mounts1, out days1);

            DateB = new DateTime(years1, mounts1, days1);
       
        long result = Math.Abs((DateA.Date - DateB.Date).Days);
    
            return result;
        }


        #region Private
        private static void ReturnDateParts(string a, out int years, out int mounts, out int days)
        {
            years = a.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()[0];
            mounts = a.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()[1];
            days = a.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()[2];
        }

        #endregion
    }
}
