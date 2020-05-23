using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBank : MonoBehaviour
{
    public float BasePower = 1000.0f;
    public float PowerPerBattery = 100.0f;

    [Tooltip("Per second")]
    public float PowerDrawPerLight = 1.0f;
    public float PowerOnCost = 25.0f;

    public float PowerLossOnFall = 50.0f;

    private int discoveredBatteries;

    private Text text;
    private NarrationManager narrationManager;

    public float CurrentPower;

    public bool IsPaused { get; set; } = false;

    private bool lessThan50Triggered = false;

    public PowerLevel CurrentPowerLevel
    {
        get
        {
            switch (CurrentPower / MaxPower)
            {
                case float powerPercentage when powerPercentage > 0.75f:
                    return PowerLevel.High;
                case float powerPercentage when powerPercentage > 0.25f:
                    return PowerLevel.Normal;
                case float powerPercentage when powerPercentage > 0.0f:
                    return PowerLevel.Low;
                default:
                    return PowerLevel.Off;
            }
        }
    }

    internal void OnPlayerFall()
    {
        CurrentPower -= PowerLossOnFall;
    }

    public float MaxPower
    {
        get
        {
            return BasePower + (PowerPerBattery * discoveredBatteries);
        }
    }

    public int ActiveLights { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentPower = MaxPower;
        discoveredBatteries = 0;

        text = GetComponent<Text>();
        narrationManager = FindObjectOfType<NarrationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsPaused)
        {
            CurrentPower -= PowerDrawPerLight * ActiveLights * Time.deltaTime;
            CurrentPower = Mathf.Max(CurrentPower, 0);

            if (!lessThan50Triggered && CurrentPower < 50 && discoveredBatteries > 0)
            {
                narrationManager.PlayConversation(Conversations.PowerBelowThreshold);
                lessThan50Triggered = true;
            }
            text.text = string.Format("{0}W", Mathf.RoundToInt(CurrentPower));
        }
    }

    public void AddBattery()
    {
        if (discoveredBatteries == 0)
        {
            narrationManager.PlayConversation(Conversations.FirstPowerCell);
        }
        else
        {
            narrationManager.PlayConversation(Conversations.FoundPower);
        }

        discoveredBatteries++;
        CurrentPower += PowerPerBattery;
    }

    public void OnObjectPowerUp()
    {
        CurrentPower -= PowerOnCost;
        CurrentPower = Mathf.Max(0, CurrentPower);
        ActiveLights++;
    }

    public void OnObjectPowerDown()
    {
        ActiveLights--;
        ActiveLights = Mathf.Max(0, ActiveLights);
    }
}

public enum PowerLevel
{
    High, Normal, Low, Off
}
