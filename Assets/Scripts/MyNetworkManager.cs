using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class MyNetworkManager : NetworkManager {
    bool firstPlayer = true;
    [SerializeField]
    Transform masterPlayerSpawnPoint;

    [SerializeField]
    private int masterID;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
    {
        NetworkMessage message = extraMessageReader.ReadMessage<NetworkMessage>();
        if (message.isMasterPlayer) { 
            GameObject player = Instantiate(spawnPrefabs[0], masterPlayerSpawnPoint.position, Quaternion.Euler(90f,0f,0f)) as GameObject;
            // Add player object for connection
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        } else
        {
            base.OnServerAddPlayer(conn, playerControllerId);
        }
        //base.OnServerAddPlayer(conn, playerControllerId);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        
        NetworkMessage test = new NetworkMessage();
        if (firstPlayer)
        {
            test.isMasterPlayer = true;
            firstPlayer = false;
        } else
        {
            test.isMasterPlayer = false;
        }

        ClientScene.AddPlayer(conn, 0, test);
    }
}
