using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PitController : MonoBehaviour
{
    private SpawnPoint[] respawns;

    private SpawnPoint activeSpawn
    {
        get
        {
            return respawns.Where(p => p.IsActive).FirstOrDefault();
        }
    }

    // Start is called before the first frame updated
    void Start()
    {
        respawns = FindObjectsOfType<SpawnPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();

        if (player)
        {
            var spawn = GetSpawnPoint();
            player.OnFall(spawn);
        }
    }

    private Vector2 GetSpawnPoint()
    {
        if (activeSpawn)
        {
            return activeSpawn.transform.position;
        }
        else
        {
            return new Vector2();
        }
    }
}
