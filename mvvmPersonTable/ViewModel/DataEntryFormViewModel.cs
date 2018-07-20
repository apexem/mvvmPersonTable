using System.Windows.Input;
using mvvmPersonTable.Model;
using System.Windows.Forms;
using System;

namespace mvvmPersonTable.ViewModel
{
    public class DataEntryFormViewModel : ViewModelBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string DateOfBirth { get; set; }

        public Person NewPerson { get; set; }

        public ICommand ButtonCommand { get; set; }

        public DataEntryFormViewModel()
        {
            ButtonCommand = new RelayCommand(ButtonHandling);
        }

        public void ButtonHandling(object obj)
        {
            switch(obj.ToString())
            {
                case "confirm":
                    this.CreatePerson();
                    break;

                case "cancel":
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<ViewModelBase>(new DataGridViewModel());
                    break;
            }
        }

        public void CreatePerson()
        {
            if (FirstName == "" || LastName == "" || StreetName == "" || HouseNumber == "" || PostalCode == "" || PhoneNumber == "" || DateOfBirth == "")
            {
                MessageBox.Show("you must fill all the info");
                return;
            }

            if (FirstName == null || LastName == null || StreetName == null || HouseNumber == null || PostalCode == null || PhoneNumber == null || DateOfBirth == null)
            {
                MessageBox.Show("you must fill all the info");
                return;
            }

            int result1, result2, result3;
            if ((!int.TryParse(HouseNumber, out result1)) && !(!int.TryParse(PhoneNumber, out result2) && !(!int.TryParse(ApartmentNumber, out result3))))
            {
                MessageBox.Show("one of the cells is numbers only");
                return;
            }

            DateTime temp = new DateTime(1, 1, 1);
            DateTime x;
            if (DateTime.TryParse(DateOfBirth, out x))
                temp = x;
            else
            {
                MessageBox.Show("wrong date format, try dd-mm-yy");
                return;
            }

            NewPerson = new Person(FirstName, LastName, StreetName, int.Parse(HouseNumber), ApartmentNumber, PostalCode, int.Parse(PhoneNumber), temp);

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<ViewModelBase>(new DataGridViewModel());
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<Person>(NewPerson);
        }
    }
}
