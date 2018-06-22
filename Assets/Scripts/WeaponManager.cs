using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {
    [SerializeField]
    private string weaponLayerName = "Weapon";
    [SerializeField]
    private PlayerWeapon primaryWeapon;

    private PlayerWeapon currentWeapon;

    private WeaponGraphics currentGraphics;

    [SerializeField]
    private Transform weaponHolder;


	void Start () {
        EquipWeapon(primaryWeapon);
	}

    void EquipWeapon(PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;
        GameObject _weaponInst = (GameObject)Instantiate(_weapon.graphics, weaponHolder.position, weaponHolder.rotation);
        _weaponInst.transform.SetParent(weaponHolder);

        currentGraphics = _weaponInst.GetComponent<WeaponGraphics>();

        _weaponInst.transform.Rotate(new Vector3(0, 90, 0));

        if (currentGraphics == null)
        {
            Debug.LogError("No weapons graphics component on the weapon object: " + _weaponInst.name);
        }

        if(isLocalPlayer)
        {
            Util.SetLayerRecursively(_weaponInst, LayerMask.NameToLayer(weaponLayerName));

        }
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public WeaponGraphics GetCurrentWeaponGraphics()
    {
        return currentGraphics;
    }

}
