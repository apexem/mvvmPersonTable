using System;

namespace mvvmPersonTable.Model
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string PostalCode { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }

        public Person()
        {
            FirstName = "Jan";
            LastName = "Kowalski";
            StreetName = "Jana Pawła";
            HouseNumber = 13;
            ApartmentNumber = "";
            PostalCode = "00-000";
            PhoneNumber = 997;
            DateOfBirth = new DateTime(97, 11, 3);
            Age = 18;
        }

        public Person(string fn, string ln, string sn, int hn, string an, string pc, int pn, DateTime date)
        {
            FirstName = fn;
            LastName = ln;
            StreetName = sn;
            HouseNumber = hn;
            if (an == null)
                ApartmentNumber = "";
            else
                ApartmentNumber = an;
            PostalCode = pc;
            PhoneNumber = pn;
            DateOfBirth = date;
            var today = DateTime.Today;
            Age = today.Year - date.Year;
        }
    }
}
