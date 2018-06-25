using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MasterPlayer : Player
{
    [SerializeField]
    private int maxEnergy = 100;

    private int currentEnergy;

    [SerializeField]
    private float energyChargeRate;

    public new void Setup()
    {
        SetDefaults();

    }

    public new void SetDefaults()
    {
        currentEnergy = maxEnergy;
        
    }
}
