using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(EnemySpawnManager))]
public class MasterPlayerSpawn : NetworkBehaviour {

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private GameObject cursor;

    [SerializeField]
    private LayerMask mask;

    private EnemySpawnManager spawnManager;

    private void Start()
    {
        if(cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced");
            this.enabled = false;
        }

        spawnManager = GetComponent<EnemySpawnManager>();
    }

    private void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Spawning enemy");
            Spawn();
        }
    }

    //This is called on server when a player shoots
    [Command]
    private void CmdOnSpawn(Vector3 _pos, Quaternion _rot)
    {
        RpcDoSpawn(_pos, _rot);
    }

    //Is called on all clients
    [ClientRpc]
    void RpcDoSpawn(Vector3 _pos, Quaternion _rot)
    {
        spawnManager.SpawnEnemy(_pos, _rot);
    }

    private void Spawn()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        //Call the on spawn method on server
        spawnManager.SpawnEnemy(cursor.transform.position,Quaternion.Euler(new Vector3(0f,cursor.transform.rotation.y,0f)));
    }
}
