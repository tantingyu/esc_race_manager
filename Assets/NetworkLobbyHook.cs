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
        SetupLocalPlayer localPlayer = gamePlayer.GetComponent<SetupLocalPlayer>();

        //link SyncVars
        localPlayer.playerName = lobby.playerName;
        localPlayer.playerColor = lobby.playerColor;
        localPlayer.playerNumber = lobby.playerNumber;
        localPlayer.racerIdx = lobby.racerIdx;
    }
}