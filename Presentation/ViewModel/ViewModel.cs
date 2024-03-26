using Presentation.ViewModel.MVVMLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        private Model.Model _model;
        private ObservableCollection<Data.Player> _players;
        public ObservableCollection<Data.Player> Players
        {
            get => _players;
            set
            {
                _players = value;
                RaisePropertyChanged();
            }
        }

        public ICommand JoinGameClick { get; set; }
        public ICommand HostGameClick { get; set; }

        public ICommand MoveUpClick { get; set; }
        public ICommand MoveDownClick { get; set; }
        public ICommand MoveLeftClick {  get; set; }
        public ICommand MoveRightClick {  get; set; }

        public ViewModel() {
            _model = new Model.Model(UpdateDisplayedPlayers);
            _model.AddPlayer();
            JoinGameClick = new RelayCommand(_model.AddPlayer);
            HostGameClick = new RelayCommand(_model.AddPlayer);
            MoveUpClick = new RelayCommand(_model.MoveUp);
            MoveDownClick = new RelayCommand(_model.MoveDown);
            MoveLeftClick = new RelayCommand(_model.MoveLeft);
            MoveRightClick = new RelayCommand(_model.MoveRight);
            UpdateDisplayedPlayers();
        }

        public void UpdateDisplayedPlayers()
        {
            Players = _model.GetPlayers();
        }
    }
}
