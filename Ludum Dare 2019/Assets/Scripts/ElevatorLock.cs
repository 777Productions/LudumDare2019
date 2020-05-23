using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ElevatorLock : MonoBehaviour
{
    private NarrationManager narrationManager;

    private KeyLight[] keyLights;

    private ElevatorControl elevatorControl;

    private AudioSource audioSource;

    private bool lockDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
        narrationManager = FindObjectOfType<NarrationManager>();
        keyLights = GetComponentsInChildren<KeyLight>();
        elevatorControl = GetComponentInParent<ElevatorControl>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UnlockElevator()
    {
        if (!lockDestroyed)
        {
            lockDestroyed = true;
            Destroy(transform.GetChild(0).gameObject);
            elevatorControl.OnUnlock();
            audioSource.Play();
            //narrationManager.PlayConversation(Conversations.ElevatorUnlocked);
        }
    }

    public bool AllKeysAquired()
    {
        return !keyLights.Any(p => !p.Key.Obtained);
    }

    public bool HasTwoKeys()
    {
        return keyLights.Count(p => p.Key.Obtained) == 2;
    }

    public bool HasFourKeys()
    {
        return keyLights.Count(p => p.Key.Obtained) == 4;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var playerController = collision.GetComponent<PlayerController>();

            if (AllKeysAquired())
            {
                UnlockElevator();
            }
        }
    }
}
