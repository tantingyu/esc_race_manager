using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook 
{
	public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager,
			GameObject lobbyPlayer, GameObject gamePlayer)
	{
	LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer> ();
	RacerController localPlayer = gamePlayer.GetComponent<RacerController> ();
	//SetupLocalPlayer localPlayer = gamePlayer.GetComponent<SetupLocalPlayer> ();

	//LINK SyncVars

	//link player name
	//localPlayer.pname = lobby.name;
	//localPlayer.playerColor = lobby.playerColor;

	//link player rigidbody
	//link player commands
	//localPlayer.menuManger = lobby.menuManger;
	}
}