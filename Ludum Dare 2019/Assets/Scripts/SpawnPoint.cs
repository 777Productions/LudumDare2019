using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class SpawnPoint : MonoBehaviour
{
    public bool IsActive { get; private set; }

    private SpawnPoint[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints = FindObjectsOfType<SpawnPoint>();
    }

    private void SetAllInactive()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.IsActive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SetAllInactive();
            IsActive = true;
        }
    }
}
