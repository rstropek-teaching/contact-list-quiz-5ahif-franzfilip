using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulAPI
{
    public class Person
    {
        public int PersonID;
        public string prename;
        public string surname;
        public string email;

        public Person(int PersonID, string prename, string surname, string email)
        {
            this.PersonID = PersonID;
            this.prename = prename;
            this.surname = surname;
            this.email = email;
        }
    }
}
