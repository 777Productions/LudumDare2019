using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PointOfInterest : MonoBehaviour
{
    public Conversations Conversation;

    private NarrationManager narrationManager;
    private bool hasTriggered = false;
    private LightSystem lightSystem;

    public bool RequireLight = false;

    private AudioSource audioSource;

    public bool InLight
    {
        get
        {
            return lightSystem.CheckInLight(transform.position);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        narrationManager = FindObjectOfType<NarrationManager>();
        lightSystem = FindObjectOfType<LightSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !hasTriggered && (!RequireLight || InLight))
        {
            narrationManager.PlayConversation(Conversation);
            hasTriggered = true;

            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
