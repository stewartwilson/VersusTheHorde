using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(MasterPlayer))]
public class MasterPlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    private string remoteLayerName = "RemotePlayer";

    [SerializeField]
    private string dontDrawLayerName = "DontDraw";

    [SerializeField]
    GameObject masterPlayerUIPrefab;
    private GameObject playerUIInstance;

    Camera sceneCamera;

    // Use this for initialization
    void Start () {
        if (!isLocalPlayer)
        {
            DisableComponents();
            AssignRemotePlayer();
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            {
                sceneCamera.gameObject.SetActive(false);
            }
            //Create player UI
            playerUIInstance = Instantiate(masterPlayerUIPrefab);
            playerUIInstance.name = masterPlayerUIPrefab.name;
        }

        GetComponent<MasterPlayer>().Setup();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnStartClient()
    {
        base.OnStartClient();

        string _netID = GetComponent<NetworkIdentity>().netId.ToString();

        MasterPlayer _player = GetComponent<MasterPlayer>();

        GameManager.RegisterPlayer(_netID, _player);
    }

    private void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }

    private void AssignRemotePlayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }
}
