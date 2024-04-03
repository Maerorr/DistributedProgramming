using Presentation.ViewModel.MVVMLight;
using System.Windows.Input;
using System.Windows.Media;
using ClientPresentation.Model;
using Presentation.Model;
using System.Diagnostics;

namespace Presentation.ViewModel
{
    public class ViewModel : ViewModelBase
    {
        private Model.Model _model;
        private List<ModelPlayer> _players;
        public List<ModelPlayer> Players
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

        string connectionStateString;

        private Brush _reactiveRectangleColor;
        public Brush ReactiveRectangleColor
        {
            get
            {
                return _reactiveRectangleColor;
            }
            set
            {
                _reactiveRectangleColor = value;
                RaisePropertyChanged();
            }
        }
        public ViewModel() {
            _model = new Model.Model(UpdateDisplayedPlayers, UpdateReactiveElements);
            _model.connectionHandler.onError += OnConnectionStateChanged;
            _model.connectionHandler.log += Log;
            _model.connectionHandler.onMessage += OnMessage;

            _model.onPlayersUpdated += UpdateDisplayedPlayers;

            OnConnectionStateChanged();

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

        public void OnMessage(string msg)
        {
            Console.WriteLine($"New message {msg}.");
        }

        public void UpdateReactiveElements(bool b)
        {
            if (b)
            {
                ReactiveRectangleColor = Brushes.Green;
            }
            else
            {
                ReactiveRectangleColor = Brushes.Blue;
            }
        }

        private void Log(string message)
        {
            Console.WriteLine(message);
        }

        private void OnConnectionStateChanged()
        {
            bool modelState = _model.connectionHandler.IsConnected();
            Trace.WriteLine($"Connection State: {modelState}");
            connectionStateString = modelState ? "Connected" : "Not Connected";

            if (!modelState)
            {
                _model.connectionHandler.Connect(new Uri(@"ws://localhost:9998"));
            }
            else
            {
                Trace.WriteLine("Requesting Update");
                _model.RequestUpdate();
            }
        }
    }
}
