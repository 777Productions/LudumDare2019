using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryItem : MonoBehaviour
{
    private BatteryBank batteryBank;

    private AudioSource audioSource;

    private SpriteRenderer[] spriteRenderers;

    private bool obtained = false;

    // Start is called before the first frame update
    void Start()
    {
        batteryBank = FindObjectOfType<BatteryBank>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && batteryBank != null && !obtained)
        {
            obtained = true;
            audioSource.Play();
            batteryBank.AddBattery();
            //Destroy(gameObject);

            foreach (var sprite in spriteRenderers)
            {
                sprite.enabled = false;
            }
        }
    }
}
