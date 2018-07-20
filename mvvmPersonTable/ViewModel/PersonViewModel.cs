using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using mvvmPersonTable.Model;
using System.Windows;
using System.Xml.Serialization;
using System.Windows.Input;

namespace mvvmPersonTable.ViewModel
{
    public class PersonViewModel
    {
        public ObservableCollection<Person> personList { get; set; }
        public Person selectedPerson { get; set; }
        private ICommand m_buttonCommand;
        public ICommand buttonCommand
        {
            get
            {
                return m_buttonCommand;
            }
            set
            {
                m_buttonCommand = value;
            }
        }

        public PersonViewModel()
        {
            loadPersons();
            buttonCommand = new RelayCommand(new Action<object>(buttonHandling));
        }

        public void buttonHandling(object obj)
        {
            switch(obj.ToString())
            {
                case "delete":
                    personList.Remove(selectedPerson);
                    break;
            }
        }

        public void loadPersons ()
        {
            personList = new ObservableCollection<Person>();
            personList.Add(new Person("karol", "423", "42342", 43242, 4234, "2342", 4234, new DateTime(1, 1, 1)));
            personList.Add(new Person("karol", "423", "42342", 43242, 4234, "2342", 4234, new DateTime(1, 1, 1)));
            personList.Add(new Person("karol", "423", "42342", 43242, 4234, "2342", 4234, new DateTime(1, 1, 1)));
            personList.Add(new Person("karol", "423", "42342", 43242, 4234, "2342", 4234, new DateTime(1, 1, 1)));
        }
    }
}


