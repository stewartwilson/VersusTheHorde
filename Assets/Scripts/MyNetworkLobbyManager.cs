using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkLobbyManager : NetworkLobbyManager {

    bool firstPlayer = true;

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        GameObject _temp;
        if (firstPlayer) { 
            _temp = (GameObject)GameObject.Instantiate(spawnPrefabs[0]);
            firstPlayer = false;
        } else {
            Transform _spawn = GetStartPosition();
            _temp = (GameObject) GameObject.Instantiate(gamePlayerPrefab);
        }


        NetworkServer.AddPlayerForConnection(conn, _temp, playerControllerId);

        return _temp;
    }
}
