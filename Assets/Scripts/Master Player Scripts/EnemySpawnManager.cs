using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawnManager : NetworkBehaviour {
    [SerializeField]
    private string enemyLayerName = "Enemy";

    private GameObject currentEnemy;

    [SerializeField]
    private List<GameObject> enemies;

    [SerializeField]
    private Transform enemyHolder;


	void Start () {
        if (enemies != null && enemies.Count > 0)
        {
            currentEnemy = enemies[1];
        }
    }

    public void SpawnEnemy(Vector3 _position, Quaternion _rotation)
    {
        GameObject _enemyInst = (GameObject)Instantiate(currentEnemy, _position, _rotation);
        _enemyInst.transform.SetParent(enemyHolder);
        NetworkServer.Spawn(_enemyInst);
    }

    public override void OnStartClient()
    {
        foreach (GameObject enemyPrefab in enemies)
        {
            ClientScene.RegisterPrefab(enemyPrefab);
        }
    }

}
