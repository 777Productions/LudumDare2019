using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorExit : MonoBehaviour
{
    private NarrationManager narrationManager;
    private GameManager gameManager;

    private bool exitTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        narrationManager = FindObjectOfType<NarrationManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !exitTriggered)
        {
            exitTriggered = true;
            gameManager.Pause();
            narrationManager.PlayConversation(Conversations.LiftFinale, gameManager.OnExitViaLift);
        }
    }
}
