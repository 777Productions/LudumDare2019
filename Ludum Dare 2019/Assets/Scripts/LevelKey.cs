using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelKey : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer shadow;
    private NarrationManager narrationManager;
    private ElevatorLock elevatorLock;

    private KeyMusic music;

    private AudioSource audioSource;

    public Color Color
    {
        get
        {
            return spriteRenderer.color;
        }
    }

    public Conversations Conversation;

    public bool Obtained { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        shadow = transform.GetChild(1).GetComponent<SpriteRenderer>();
        narrationManager = FindObjectOfType<NarrationManager>();
        elevatorLock = FindObjectOfType<ElevatorLock>();
        music = FindObjectOfType<KeyMusic>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !Obtained)
        {
            audioSource.Play();
            spriteRenderer.enabled = false;
            shadow.enabled = false;
            Obtained = true;

            if (elevatorLock.HasTwoKeys())
            {
                music.OnTwoKeys();

            }
            else if (elevatorLock.HasFourKeys())
            {
                music.OnFourKeys();
            }

            narrationManager.PlayConversation(Conversation);

            if (elevatorLock.AllKeysAquired())
            {
                narrationManager.PlayConversation(Conversations.AllKeysObtained);
            }
        }
    }
}
