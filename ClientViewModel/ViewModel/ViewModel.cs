using ClientViewModel.Model;
using ClientViewModel.ViewModel.MVVMLight;
using System.Windows.Input;

namespace ClientViewModel.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        private IModel model;
        private List<ViewModelPlayer> _players = new List<ViewModelPlayer>();
        public List<ViewModelPlayer> Players
        {
            get => _players;
            set
            {
                _players = value;
                RaisePropertyChanged();
            }
        }

        public ICommand MoveUpClick { get; set; }
        public ICommand MoveDownClick { get; set; }
        public ICommand MoveLeftClick { get; set; }
        public ICommand MoveRightClick { get; set; }

        public ViewModel()
        {
            model = IModel.Create();

            MoveUpClick = new RelayCommand(model.MoveUp);
            MoveDownClick = new RelayCommand(model.MoveDown);
            MoveLeftClick = new RelayCommand(model.MoveLeft);
            MoveRightClick = new RelayCommand(model.MoveRight);

            Players.Add(new ViewModelPlayer("John", 50, 50, 5));
        }
    }
}
