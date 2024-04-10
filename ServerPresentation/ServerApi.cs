
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalApi
{
    public static class Headers
    {
        public static string GetPlayersCommand = "GetPlayers";
        public static string MovePlayerCommand = "MovePlayer";
        public static string JoinResponse = "JoinResponse";
        public static string UpdatePlayersResponse = "UpdatePlayers";
        public static string MovePlayerResponse = "MovePlayerResponse";
    }

    public abstract partial class ServerCommand
    {
        public string Header { get; set; }
    }

    [Serializable]
    public class GetPlayersCommand : ServerCommand
    {
    }

    [Serializable]
    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right,
    }

    public partial class MovePlayerCommand : ServerCommand
    {
        public System.Guid TransactionId { get; set; }
        public System.Guid PlayerId { get; set; }
        public MoveDirection Direction { get; set; }
    }

    public abstract partial class ServerResponse
    {
        public string Header { get; set; }
    }

    public partial class JoinResponse : ServerResponse
    {
        public System.Guid GuidForPlayer { get; set; }
    }

    public partial class PlayerData
    {
        public string Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Speed { get; set; }
    }

    public partial class UpdatePlayersResponse : ServerResponse
    {
        public System.Collections.Generic.ICollection<PlayerData> Players { get; set; }
    }

    public partial class MovePlayerResponse : ServerResponse
    {
        public System.Guid TransactionId { get; set; }
        public bool IsSuccess { get; set; }
    }
}
