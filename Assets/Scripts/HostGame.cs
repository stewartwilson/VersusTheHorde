using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {

    [SerializeField]
    private uint roomSize = 5;
    [SerializeField]
    private string roomName;

    private NetworkManager networkManager;

    private void Start()
    {
        networkManager = NetworkLobbyManager.singleton;
        if(networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }
    }

    public void SetRoomName(string _name)
    {
        roomName = _name;
    }

    public void CreateRoom()
    {
        if(roomName != null && roomName != "")
        {
            Debug.Log("Creating Room: " + roomName + " with room for " + roomSize + " players");
            networkManager.matchMaker.CreateMatch(roomName,roomSize,true,"","","",0,0,networkManager.OnMatchCreate);
        }
    }

}
