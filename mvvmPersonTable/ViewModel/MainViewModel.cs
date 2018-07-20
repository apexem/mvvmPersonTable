using GalaSoft.MvvmLight;
using mvvmPersonTable.Model;

namespace mvvmPersonTable.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public DataGridViewModel MainWindowVM { get; set; }

        private ViewModelBase _CurrentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set
            {
                _CurrentViewModel = value;
                OnPropertyChanged("CurrentViewModel");
            }
        }
        public MainViewModel()
        {
            MainWindowVM = new DataGridViewModel();
            CurrentViewModel = MainWindowVM;
            GalaSoft.MvvmLight.Messaging.Messenger.Default.Register<ViewModelBase>(this, this.HandleViewModelMessage);
        }
        public void HandleViewModelMessage(ViewModelBase message)
        {
            if (message.GetType() == typeof(DataEntryFormViewModel))
            {
                CurrentViewModel = new DataEntryFormViewModel();
            }

            if (message.GetType() == typeof(DataGridViewModel))
            {
                CurrentViewModel = MainWindowVM;
            }
        }
    }
}