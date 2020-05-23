using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DZNavLight : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private static System.Random random = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

        transform.Rotate(0, 0, random.Next(360));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            spriteRenderer.enabled = true;
        }
    }
}
