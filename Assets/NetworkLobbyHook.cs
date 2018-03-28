using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager,
            GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        RacerController localPlayer = gamePlayer.GetComponent<RacerController>();

        //link SyncVars
        lobby.playerName = localPlayer.playerName; //player name
        lobby.playerNumber = localPlayer.playerNumber; //player number
        lobby.racerIdx = localPlayer.racerIdx; //player racer
    }
}