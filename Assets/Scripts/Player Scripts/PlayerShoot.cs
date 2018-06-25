using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(WeaponManager))]
public class PlayerShoot : NetworkBehaviour {

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    private WeaponManager weaponManager;

    private PlayerWeapon currentWeapon;

    private void Start()
    {
        if(cam == null)
        {
            Debug.LogError("PlayerShoot: No camera referenced");
            this.enabled = false;
        }

        weaponManager = GetComponent<WeaponManager>();
    }

    private void Update()
    {
        currentWeapon = weaponManager.GetCurrentWeapon();
        
        if (currentWeapon.rateOfFire <= 0f)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        } else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                InvokeRepeating("Shoot", 0f, 1f/currentWeapon.rateOfFire);
            } else if(Input.GetButtonUp("Fire1"))
            {
                CancelInvoke("Shoot");
            }
        }
    }

    //This is called on server when a player shoots
    [Command]
    private void CmdOnShoot()
    {
        RpcDoShootEffect();
    }

    //This is called on all clietns when a a shoot effect
    // is needed
    [ClientRpc]
    private void RpcDoShootEffect()
    {
        weaponManager.GetCurrentWeaponGraphics().muzzleFlash.Play();
    }

    //Is called on the server when soemthing is hit
    [Command]
    void CmdOnHit(Vector3 _pos, Vector3 _normal)
    {
        RpcDoHitEffect(_pos, _normal);
    }

    //Is called on all clients
    [ClientRpc]
    void RpcDoHitEffect(Vector3 _pos, Vector3 _normal)
    {
        GameObject hitEffect = Instantiate(weaponManager.GetCurrentWeaponGraphics().hitEffectPrefab, 
            _pos, Quaternion.LookRotation(_normal));

        Destroy(hitEffect, 2f);
    }

    [Client]
    private void Shoot()
    {
        if(!isLocalPlayer)
        {
            return;
        }
        //Call the on shoot method on server
        CmdOnShoot();

        RaycastHit _hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, currentWeapon.range, mask))
        {
            if(_hit.collider.tag == "Enemy")
            {
                CmdEnemyShot(_hit.collider.name, currentWeapon.damage);
            }

            // gets the location of the bullet hit
            CmdOnHit(_hit.point, _hit.normal);
        }
    }

    [Command]
    private void CmdPlayerShot(string _playerID, int _damage)
    {
        Debug.Log(_playerID + " has been shot");

        Player _player = GameManager.GetPlayer(_playerID);

        _player.RpcTakeDamage(_damage);
    }

    [Command]
    private void CmdEnemyShot(string _enemyID, int _damage)
    {
        Debug.Log(_enemyID + " has been shot");

        Enemy _enemy = GameManager.GetEnemy(_enemyID);

        _enemy.RpcTakeDamage(_damage);
    }
}
