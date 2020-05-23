using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1.0f;

    private Rigidbody2D rb2d;
    private NarrationManager narrationManager;
    private BatteryBank batteryBank;
    private MusicManager musicManager;
    private GameManager gameManager;
    private Animator[] animators;
    private Collider2D playerCollider;
    private LightSystem lightSystem;

    private SpriteRenderer[] spriteRenderers;

    private bool IsFalling;

    public bool InDarkness
    {
        get
        {
            return !lightSystem.CheckInLight(transform.position);
        }
    }
    private bool darknessTriggered;


    private bool isIdle;
    public bool IsIdle
    {
        get { return isIdle; }
        set
        {
            SetAnimatorBools("isIdle", value);
            isIdle = value;
        }
    }

    private bool isMovingUp;
    public bool IsMovingUp
    {
        get { return isMovingUp; }
        set
        {
            SetAnimatorBools("isMovingUp", value);
            isMovingUp = value;
        }
    }

    private bool isMovingDown;
    public bool IsMovingDown
    {
        get { return isMovingDown; }
        set
        {
            SetAnimatorBools("isMovingDown", value);
            isMovingDown = value;
        }
    }

    private bool isMovingSideways;
    public bool IsMovingSideways
    {
        get { return isMovingSideways; }
        set
        {
            SetAnimatorBools("isMovingSideways", value);
            isMovingSideways = value;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        animators = GetComponentsInChildren<Animator>();
        playerCollider = GetComponent<Collider2D>();

        gameManager = FindObjectOfType<GameManager>();
        musicManager = FindObjectOfType<MusicManager>();
        narrationManager = FindObjectOfType<NarrationManager>();
        batteryBank = FindObjectOfType<BatteryBank>();
        lightSystem = FindObjectOfType<LightSystem>();
    }

    void FixedUpdate()
    {
        HandleDarknessStates();

        if (gameManager.GameState != GameState.Running)
        {
            rb2d.velocity = Vector2.zero;
            IsIdle = true;
            IsMovingDown = false;
            IsMovingUp = false;
            IsMovingSideways = false;
            return;
        }

        if (IsFalling)
        {
            HandleFallingState();
        }
        else
        {
            HandleMovement();
        }
    }

    private void HandleDarknessStates()
    {
        if (InDarkness && !darknessTriggered)
        {
            darknessTriggered = true;
            musicManager.OnEnterDark();
        }

        else if (!InDarkness && darknessTriggered)
        {
            darknessTriggered = false;
            musicManager.OnEnterLight();
        }
        //InDarkness = true;
    }

    private void HandleMovement()
    {
        UpdateAnimationFlags();


        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector2 movement;

        if (Mathf.Abs(vAxis) > Mathf.Abs(hAxis))
        {
            movement = Vector2.up * vAxis;
        }
        else
        {
            movement = Vector2.right * hAxis;
        }

        rb2d.velocity = (movement * Speed);
    }

    //public void SetIlluminated(bool isIlluminated)
    //{
    //    InDarkness = !isIlluminated;
    //}

    private void SetAnimatorBools(string boolName, bool value)
    {
        foreach(var animator in animators)
        {
            animator.SetBool(boolName, value);
        }
    }

    private void UpdateAnimationFlags()
    {
        var x = rb2d.velocity.x;
        var y = rb2d.velocity.y;

        IsIdle = Math.Abs(x + y) < Mathf.Epsilon;

        IsMovingUp = y > 0.05f && Math.Abs(y) > Math.Abs(x);
        IsMovingDown = y < -0.05f && Math.Abs(y) > Math.Abs(x);
        IsMovingSideways = Math.Abs(x) > 0.05f && Math.Abs(x) > Math.Abs(y);

        if (IsMovingSideways && Mathf.Abs(x) > 0.05f)
        {
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.flipX = x > 0;
            }
        }
    }

    private void HandleFallingState()
    {
    }

    internal void OnFall(Vector2 spawnPoint)
    {
        IsFalling = true;
        batteryBank.IsPaused = true;
        narrationManager.PlayConversation(Conversations.Falling);
        StartCoroutine("RespawnAfterFall", spawnPoint);
    }

    private IEnumerator RespawnAfterFall(Vector2 spawnPoint)
    {
        for (int i = 0; i < 20; i++)
        {
            transform.localScale *= 0.75f;
            rb2d.velocity *= 0.5f;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(1);

        transform.position = spawnPoint;

        batteryBank.OnPlayerFall();
        narrationManager.PlayConversation(Conversations.Respawn);
        batteryBank.IsPaused = false;
        IsFalling = false;
        transform.localScale = Vector2.one;
        rb2d.velocity = Vector2.zero;
    }
}
