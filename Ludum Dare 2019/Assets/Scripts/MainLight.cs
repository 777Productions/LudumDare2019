using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainLight : PowerableObject
{
    public Sprite LightOn;
    public Sprite LightOff;

    public static int seed = 0;

    public bool startState = false;

    public bool Flicker = false;

    public bool InDarkZone
    {
        get
        {
            return CheckInDarkZone();
        }
    }

    private static bool FirstTime5 = true;
    private static int totalTriggers = 0;

    private GameManager gameManager;

    private LightSystem lightSystem;
    public Collider2D Collider { get; private set; }

    private NarrationManager narrationManager;

    [Range(0, 1)]
    public float flickerChance = 0.01f;

    private SpriteRenderer spriteRenderer;

    private System.Random random = new System.Random(seed);
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<Collider2D>();
        batteryBank = FindObjectOfType<BatteryBank>();
        narrationManager = FindObjectOfType<NarrationManager>();
        state = startState;

        //InDarkZone = CheckInDarkZone();

        lightSystem = GetComponentInParent<LightSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Flicker && random.NextDouble() <= flickerChance)
        {
            spriteRenderer.sprite = LightOff;
        }
        else
        {
            spriteRenderer.sprite = IsActive() ? LightOn : LightOff;
        }
    }

    private bool CheckInDarkZone()
    {
        var darkZones = GameObject.FindGameObjectsWithTag("Dark Zone");

        foreach(var zone in darkZones)
        {
            var dzCollider = zone.GetComponent<Collider2D>();

            if (dzCollider.bounds.Intersects(GetComponent<Collider2D>().bounds))
            {
                return true;
            }
        }

        return false;
    }

    public void SwitchLight()
    {
        if (InDarkZone)
        {
            return;
        }

        if (batteryBank.CurrentPower < batteryBank.PowerOnCost && state == false)
        {
            //narrationManager.PlayConversation(Conversations.NotEnoughPower);
            return;
        }

        if (state == false)
        {
            totalTriggers++;
        }

        if (totalTriggers == 10 && state == false)
        {
            var closestLight = lightSystem.GetClosestLight(this);
            closestLight.ToggleState();

            narrationManager.PlayConversation(Conversations.ThirdLightIncorrect);
            return;
        }

        ToggleState();

        if (FirstTime5 && lightSystem.ActiveLightCount == 5)
        {
            FirstTime5 = false;
            narrationManager.PlayConversation(Conversations.FiveRoomsLit);
        }
    }

    private void OnMouseDown()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}
        if (gameManager.GameState == GameState.Running)
        {
            SwitchLight();
        }
    }

    //void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (IsActive() && collision.tag == "Player")
    //    {
    //        var playerController = collision.gameObject.GetComponent<PlayerController>();

    //        playerController.SetIlluminated(true);
    //    }
    //}
}
