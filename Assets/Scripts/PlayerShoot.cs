using UnityEngine;
using UnityEngine.Networking;

public class PlayerShoot : NetworkBehaviour {

    public PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private void Start()
    {
        if(cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    private void Shoot()
    {
        RaycastHit _hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            if(_hit.collider.tag == "Enemy")
            {
                CmdEnemyShot(_hit.collider.name, weapon.damage);
            }
        }
    }

    [Command]
    private void CmdPlayerShot(string _playerID, int _damage)
    {
        Debug.Log(_playerID + " has been shot");

        Player _player = GameManager.GetPlayer(_playerID);

        _player.TakeDamage(_damage);
    }

    [Command]
    private void CmdEnemyShot(string _enemyID, int _damage)
    {
        Debug.Log(_enemyID + " has been shot");

        Enemy _enemy = GameManager.GetEnemy(_enemyID);

        _enemy.TakeDamage(_damage);
    }
}
