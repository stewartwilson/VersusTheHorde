using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {

    [SerializeField]
    Transform masterPlayerSpawnPoint;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (numPlayers == 0)
        {
            GameObject player = Instantiate(spawnPrefabs[0], masterPlayerSpawnPoint.position, Quaternion.Euler(90f,0f,0f)) as GameObject;
            // Add player object for connection
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        } else
        {
            base.OnServerAddPlayer(conn, playerControllerId);
        }
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        ClientScene.RegisterPrefab(spawnPrefabs[0]);
        ClientScene.AddPlayer(conn, 0);
    }
}
