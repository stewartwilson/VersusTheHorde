using UnityEngine;
using UnityEngine.Networking;

public class MasterPlayerAssign : MonoBehaviour {

    public NetworkManager networkManager;
    [SerializeField]
    private GameObject masterPlayerPrefab;

    private void Start()
    {
        networkManager = NetworkLobbyManager.singleton;
        
    }

}
