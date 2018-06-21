using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    public void Setup ()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }
        SetDefaults();
    }

    [ClientRpc]
    public void RpcTakeDamage(int _damage)
    {
        if(isDead)
        {
            return;
        }
        currentHealth -= _damage;

        Debug.Log(transform.name + " now has " + currentHealth);

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;

        //Disable components
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        Collider[] _col = GetComponents<Collider>();
        if (_col != null && _col.Length > 0)
        {
            for (int i = 0; i < _col.Length; i++)
            {
                _col[i].enabled = false;
            }

        }        

        Debug.Log(transform.name + " is Dead");

        //Call respawn if respawn enabled
        StartCoroutine(Respawn());
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;
        isDead = false;

        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        Collider[] _col = GetComponents<Collider>();
        if(_col != null && _col.Length > 0)
        {
            for (int i = 0; i < _col.Length; i++)
            {
                _col[i].enabled = true;
            }
            
        }
    }

    private IEnumerator Respawn()
    {
        if (GameManager.instance.matchSettings.respawnEnabled)
        {
            yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

            SetDefaults();
            Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();

            transform.position = _spawnPoint.position;
            transform.rotation = _spawnPoint.rotation;
        }
    }
}
