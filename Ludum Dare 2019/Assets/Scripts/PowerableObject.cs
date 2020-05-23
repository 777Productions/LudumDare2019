using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerableObject : MonoBehaviour, IPowerable
{
    protected bool state;

    protected BatteryBank batteryBank;

    //public float PowerOnCost = 25;
    //public float Wattage = 100;
            
    // Start is called before the first frame update
    void Start()
    {
        state = false;
    }

    public bool IsPowered()
    {
        return batteryBank.CurrentPowerLevel != PowerLevel.Off;
    }

    public bool IsActive()
    {
        return state && IsPowered();
    }

    public void SetState(bool state)
    {
        if (!this.state && state)
        {
            batteryBank.OnObjectPowerUp();
        }
        else
        {
            batteryBank.OnObjectPowerDown();
        }

        this.state = state;
    }

    public void ToggleState()
    {
        SetState(!state);
    }
}
