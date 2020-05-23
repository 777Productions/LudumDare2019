using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip WhiteNoiseClip;
    public AudioClip BackgroundMusicClip;
    public AudioClip ShepardToneClip;

    private float backgroundMusicMaxVolume;
    private float shepardToneMaxVolume;

    private AudioSource[] audioSources;

    //private bool interrupt = false;
    private bool isFading = false;
    private bool interrupt = false;

    public AudioSource WhiteNoise
    {
        get
        {
            return audioSources[0];
        }
    }

    public AudioSource BackgroundMusic
    {
        get
        {
            return audioSources[1];
        }
    }

    public AudioSource ShepardTone
    {
        get
        {
            return audioSources[2];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();

        WhiteNoise.clip = WhiteNoiseClip;
        BackgroundMusic.clip = BackgroundMusicClip;
        ShepardTone.clip = ShepardToneClip;

        backgroundMusicMaxVolume = BackgroundMusic.volume;
        shepardToneMaxVolume = ShepardTone.volume;

        ShepardTone.volume = 0;
        BackgroundMusic.volume = 0;

        WhiteNoise.Play();
        BackgroundMusic.Play();
        ShepardTone.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFirstLightTurnedOn()
    {
        BackgroundMusic.volume = 0;
        //BackgroundMusic.Play();

        var args = new FadeArgs
        {
            Source = BackgroundMusic,
            MaxVolume = backgroundMusicMaxVolume,
            Speed = 0.01f
        };

        StartCoroutine("FadeSourceIn", args);
    }

    public void OnEnterDark()
    {
        interrupt |= isFading;

        var args = new FadeArgs
        {
            Source = ShepardTone,
            MaxVolume = shepardToneMaxVolume,
            Speed = 0.01f
        };

        StartCoroutine("FadeSourceIn", args);
    }

    public void OnEnterLight()
    {
        interrupt |= isFading;

        var args = new FadeArgs
        {
            Source = ShepardTone,
            MaxVolume = shepardToneMaxVolume,
            Speed = 0.025f
        };

        StartCoroutine("FadeSourceOut", args);
    }

    public void IncreaseUrgency()
    {

    }

    public void DecreaseUrgency()
    {

    }

    private IEnumerator FadeSourceIn(FadeArgs args)
    {
        while (isFading && args.Source == ShepardTone)
        {
            yield return new WaitForSeconds(0.01f);
        }

        if (args.Source == ShepardTone)
        {
            isFading = true;
        }

        for (float i = args.Source.volume; i < args.MaxVolume; i += args.Speed)
        {
            if (interrupt && args.Source == ShepardTone)
            {
                interrupt = false;
                break;
            }

            args.Source.volume = i;
            yield return new WaitForSeconds(0.05f);
        }

        if (args.Source == ShepardTone)
        {
            isFading = false;
        }
    }

    private IEnumerator FadeSourceOut(FadeArgs args)
    {
        while (isFading && args.Source == ShepardTone)
        {
            yield return new WaitForSeconds(0.01f);
        }

        if (args.Source == ShepardTone)
        {
            isFading = true;
        }

        for (float i = args.Source.volume; i > args.MinVolume; i -= args.Speed)
        {
            if (interrupt && args.Source == ShepardTone)
            {
                interrupt = false;
                break;
            }

            args.Source.volume = i;
            yield return new WaitForSeconds(0.05f);
        }

        if (args.Source == ShepardTone)
        {
            isFading = false;
        }
    }
}

public class FadeArgs
{
    public AudioSource Source { get; set; }
    public float Speed { get; set; }
    public float MaxVolume { get; set; }
    public float MinVolume { get; set; }
}
