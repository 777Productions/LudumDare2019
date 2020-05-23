using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private static System.Random random = new System.Random();

    public AudioClip[] Footsteps;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayFootstep()
    {
        int index = random.Next(Footsteps.Length);

        audioSource.clip = Footsteps[index];
        audioSource.Play();
    }
}
