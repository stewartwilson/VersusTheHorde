using UnityEngine;

[System.Serializable]
public class PlayerWeapon {

    public string name = "Gun";
    public int damage = 40;
    public float range = 100f;

    public float rateOfFire = 0f;

    public GameObject graphics;

    public Vector3 rotationOffset;



}
