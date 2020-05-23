using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLight : MonoBehaviour
{
    public LevelKey Key;

    //private SpriteRenderer spriteRenderer;
    private SpriteRenderer glowSprite;

    // Start is called before the first frame update
    void Start()
    {
        //spriteRenderer = GetComponent<SpriteRenderer>();

        glowSprite = transform.GetComponent<SpriteRenderer>();

        //spriteRenderer.color = Key.Color;
        glowSprite.color = Key.Color;
    }

    // Update is called once per frame
    void Update()
    {
        glowSprite.enabled = Key.Obtained;
    }
}
