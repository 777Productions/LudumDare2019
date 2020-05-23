using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Game.ChatSystem;

[RequireComponent(typeof(StoryManager))]
public class GameManager : MonoBehaviour
{
    private StoryManager storyManager;
    private MusicManager musicManager;

    public GameState GameState { get; private set; }

    private PlayerController player;
    private BatteryBank batteryBank;

    private LightSystem lightSystem;

    private bool triggeredDarkDeath;
    private float timeInDarkness;

    public bool skipIntro = false;

    public float DarknessDeathTime = 5.0f;

    public Image blackImage;
    private bool deathTriggered = false;

    public Text winText;
    public Text gameOverText;
    public Text Instructions;
    public Text playAgain;

    private void Awake()
    {
        storyManager = GetComponent<StoryManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Instructions.enabled = false;
        musicManager = FindObjectOfType<MusicManager>();
        GameState = GameState.Introduction;
        player = FindObjectOfType<PlayerController>();
        lightSystem = FindObjectOfType<LightSystem>();
        batteryBank = FindObjectOfType<BatteryBank>();
        blackImage.enabled = false;
        winText.enabled = false;
        gameOverText.enabled = false;
        playAgain.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (skipIntro)
        {
            OnIntroductionFinished();
            OnFirstLightFinished();
            OnLiftBriefingFinished();
            skipIntro = false;
        }

        switch (GameState)
        {
            case GameState.Introduction:
                HandleIntroduction();
                break;
            case GameState.GameStart:
                HandleGameStart();
                break;
            case GameState.Paused:
            // Do Nothing
            case GameState.Running:
                HandleGameRunning();
                break;
            case GameState.Win:
                HandleWin();
                break;
            case GameState.GameOver:
                HandleGameOver();
                break;
        }
    }

    private void HandleIntroduction()
    {
        //storyManager.PlayConversation(Conversations.Introduction, OnIntroductionFinished);
        storyManager.PlayIntro();
        GameState = GameState.Paused;
    }

    private void HandleGameStart()
    {
        GameState = GameState.Running;
    }

    private void HandleGameRunning()
    {
        if (player.InDarkness && Mathf.Abs(batteryBank.CurrentPower) < batteryBank.PowerOnCost && lightSystem.ActiveLightCount == 0)
        {
            if (!triggeredDarkDeath)
            {
                timeInDarkness = 0;
                triggeredDarkDeath = true;
                musicManager.IncreaseUrgency();
            }

            timeInDarkness += Time.deltaTime;

            if (!deathTriggered && timeInDarkness >= DarknessDeathTime)
            {
                deathTriggered = true;
                GameState = GameState.Paused;
                //narrationManager.PlayConversation(Conversations.DarkNoPower, OnDeathByDarkness);
            }
        }

        if (!player.InDarkness || batteryBank.CurrentPower >= batteryBank.PowerOnCost)
        {
            if (triggeredDarkDeath)
            {
                triggeredDarkDeath = false;
                musicManager.DecreaseUrgency();
            }
        }
    }

    private void HandleWin()
    {
        if (Input.GetButton("Jump"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void HandleGameOver()
    {
        if (Input.GetButton("Jump"))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnIntroductionFinished()
    {
        lightSystem.TurnOnElevatorLight();
        musicManager.OnFirstLightTurnedOn();

        //if (!skipIntro)
            //narrationManager.PlayConversation(Conversations.FirstLight, OnFirstLightFinished);
    }

    private void OnFirstLightFinished()
    {
        if (!skipIntro)
        {
            Instructions.enabled = true;
            //narrationManager.PlayConversation(Conversations.LiftBriefing, OnLiftBriefingFinished);
        }
    }

    private void OnLiftBriefingFinished()
    {
        GameState = GameState.GameStart;
        Instructions.enabled = false;
    }

    private void OnDeathByDarkness()
    {
        GameState = GameState.GameOver;
        SetScreenToBlack();
        playAgain.enabled = true;
        gameOverText.enabled = true;
    }

    public void Pause()
    {
        GameState = GameState.Paused;
    }

    public void OnExitViaLift()
    {
        GameState = GameState.Win;
        SetScreenToBlack();
        playAgain.enabled = true;
        winText.enabled = true;
    }

    private void SetScreenToBlack()
    {
        blackImage.enabled = true;
    }
}

public enum GameState
{
    Introduction, GameStart, Paused, Running, GameOver, Win
}
