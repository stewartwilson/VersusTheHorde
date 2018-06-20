using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Enemy))]
public class EnemySetup : NetworkBehaviour
{ 
    [SerializeField]
    private string layerName = "Enemy";


    private void Start()
    {
        string _netID = GetComponent<NetworkIdentity>().netId.ToString();

        Enemy _enemy= GetComponent<Enemy>();

        GameManager.RegisterEnemy(_netID, _enemy);
    }

    private void OnDisable()
    {

        GameManager.UnRegisterEnemy(transform.name);
    }
}

