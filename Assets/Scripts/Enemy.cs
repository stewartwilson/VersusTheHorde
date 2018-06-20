using UnityEngine;
using UnityEngine.Networking;

public class Enemy : NetworkBehaviour {

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    private void Awake()
    {
        SetDefaults();
    }

    public void TakeDamage(int _damage)
    {
        currentHealth -= _damage;

        Debug.Log(transform.name + " now has " + currentHealth);
    }

    public void SetDefaults()
    {
        currentHealth = maxHealth;
    }
}
