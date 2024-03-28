using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;

namespace Presentation.Model
{
    public class ModelPlayer : INotifyPropertyChanged
    {
        public float X { get; private set; }
        public float Y { get; private set; }

        public ModelPlayer(ILogicPlayer player)
        {
            X = player.X;
            Y = player.Y;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}