using UnityEngine;
using UnityEngine.Networking;

public class WeaponManager : NetworkBehaviour {
    [SerializeField]
    private string weaponLayerName = "Weapon";
    [SerializeField]
    private PlayerWeapon primaryWeapon;

    private PlayerWeapon currentWeapon;

    [SerializeField]
    private Transform weaponHolder;


	void Start () {
        EquipWeapon(primaryWeapon);
	}

    void EquipWeapon(PlayerWeapon _weapon)
    {
        currentWeapon = _weapon;
        GameObject _weaponInst = (GameObject)Instantiate(_weapon.graphics, weaponHolder.position,Quaternion.Euler(_weapon.rotationOffset));
        _weaponInst.transform.SetParent(weaponHolder);

        if(isLocalPlayer)
        {
            _weaponInst.layer = LayerMask.NameToLayer(weaponLayerName);
        }
    }

    public PlayerWeapon GetCurrentWeapon()
    {
        return currentWeapon;
    }
	
}
