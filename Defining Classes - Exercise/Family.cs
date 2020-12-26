using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefiningClasses
{
    public class Family
    {
        public List<Person> People { get; set; }

        public Family()
        {
            this.People = new List<Person>();
        }

        public void AddMember(Person member)
        {
            People.Add(member);
        }

        public Person GetOldestMember()
        {
            Person reserve = People.OrderByDescending(x => x.Age).FirstOrDefault();
            
            return reserve;
        }

        public Person[] OverThirtyYearsOld()
        {
            Person[] reserve = People.Where(p => p.Age > 30).OrderBy(p=>p.Name).ToArray();
            return reserve;
        }

    }
}
