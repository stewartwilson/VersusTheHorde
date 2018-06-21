using UnityEngine;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour {

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

    public void Start()
    {
        Setup();
    }

    public void Setup ()
    {
        SetDefaults();
    }

    [ClientRpc]
    public void RpcTakeDamage(int _damage)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= _damage;

        Debug.Log(transform.name + " now has " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;

        //Disable components

        Debug.Log(transform.name + " is Dead");

        //Call respawn

    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;
        isDead = false;
    }
}
