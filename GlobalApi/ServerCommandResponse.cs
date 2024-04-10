﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalApi
{
    [Serializable]
    public abstract class ServerCommand
    {
        public string header;

        protected ServerCommand(string header)
        {
            this.header = header;
        }
    }

    [Serializable]
    public class GetPlayersCommand : ServerCommand
    {
        public static string HEADER = "GetPlayers";
        public GetPlayersCommand() : base(HEADER) { }
    }

    [Serializable]
    public class MovePlayerCommand : ServerCommand
    {
        public static string HEADER = "MovePlayer";
        public Guid transactionId;
        public Guid playerId;
        public float x;
        public float y;

        public MovePlayerCommand(Guid playerId, float x, float y) : base(HEADER)
        {
            transactionId = Guid.NewGuid();
            this.playerId = playerId;
            this.x = x;
            this.y = y;
        }
    }

    [Serializable]
    public abstract class ServerResponse
    {
        public string header { get; private set; }

        protected ServerResponse(string header)
        {
            this.header = header;
        }
    }

    [Serializable]
    public struct PlayerData
    {
        public string name;
        public float x;
        public float y;

        public PlayerData(string name, float x, float y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }
    }

    [Serializable]
    public class UpdatePlayersResponse : ServerResponse
    {
        public static readonly string HEADER = "UpdatePlayers";
        public PlayerData[] players;

        public UpdatePlayersResponse() : base(HEADER)
        { }
    }

    [Serializable]
    public class MovePlayerResponse : ServerResponse
    {
        public static readonly string HEADER = "MovePlayerResponse";
        public Guid transactionId;
        public bool isSuccess;

        public MovePlayerResponse() : base(HEADER)
        {
        }
    }
}