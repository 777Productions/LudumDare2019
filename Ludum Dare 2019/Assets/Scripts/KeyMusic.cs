using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMusic : MonoBehaviour
{
    private AudioSource[] audioSources;

    private float[] maxVolume = new float[2];

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();

        maxVolume[0] = audioSources[0].volume;
        maxVolume[1] = audioSources[1].volume;

        audioSources[0].volume = 0;
        audioSources[1].volume = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTwoKeys()
    {
        StartCoroutine("FadeSourceFirstIn");
    }

    public void OnFourKeys()
    {
        StartCoroutine("FadeSourceSecondIn");
    }

    private IEnumerator FadeSourceFirstIn()
    {
        for (float i = audioSources[0].volume; i < maxVolume[0]; i += 0.01f)
        {
            audioSources[0].volume = i;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator FadeSourceSecondIn()
    {
        for (float i = audioSources[1].volume; i < maxVolume[1]; i += 0.01f)
        {
            audioSources[1].volume = i;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
