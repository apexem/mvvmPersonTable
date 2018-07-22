using System;
using System.Collections.ObjectModel;
using mvvmPersonTable.Model;
using System.Windows.Input;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Data;

namespace mvvmPersonTable.ViewModel
{
    public class DataGridViewModel : ViewModelBase
    {
        private string Path { get; set; }
        public ICommand SelectedCellChangedCommand { get; set; }
        public ICommand ButtonCommand { get; set; }
        public ObservableCollection<Person> PersonList { get; set; }
        public Person SelectedPerson { get; set; }

        private bool isChanged;
        public bool IsChanged
        {
            get { return isChanged; }
            set
            {
                isChanged = value;
                OnPropertyChanged("IsChanged");
            }
        }

        public DataGridViewModel()
        {
            IsChanged = false;
            PersonList = new ObservableCollection<Person>();
            Path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "/SavedCollection.xml";

            if (File.Exists(Path))
                LoadData();       
            
            ButtonCommand = new RelayCommand(new Action<object>(ButtonHandling));
            SelectedCellChangedCommand = new RelayCommand(new Action<object>(SelectedCellChanged));

            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<Person>(this, this.HandlePersonMessage);
        }
        
        public void HandlePersonMessage(Person message)
        {
            PersonList.Add(message);
            IsChanged = true;
        }

        public void ButtonHandling(object obj)
        {
            switch(obj.ToString())
            {
                case "delete":
                    if (SelectedPerson != null)
                    {
                        PersonList.Remove(SelectedPerson);
                        IsChanged = true;
                    }
                    break;

                case "add":
                    GalaSoft.MvvmLight.Messaging.Messenger.Default.Send<ViewModelBase>(new DataEntryFormViewModel());
                    break;

                case "cancel":
                    LoadData();
                    break;

                case "save":
                    SaveData();
                    break;
            }
        }

        public void SelectedCellChanged(object obj)
        {
            IsChanged = true;
        }

        public void SaveData()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Person>));
            using (StreamWriter wr = new StreamWriter(Path))
            {
                xs.Serialize(wr, PersonList);
            }
            IsChanged = false;
        }

        public void LoadData()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Person>));
            using (StreamReader rd = new StreamReader(Path))
            {
                ObservableCollection<Person> temp = xs.Deserialize(rd) as ObservableCollection<Person>;

                PersonList.Clear();

                foreach (Person person in temp)
                {
                    PersonList.Add(person);
                }
            }
            IsChanged = false;
        }
    }
}


