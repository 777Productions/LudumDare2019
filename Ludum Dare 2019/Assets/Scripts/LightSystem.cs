using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class LightSystem : MonoBehaviour
{
    public GameObject LightPrefab;
    public Vector2Int Dimensions = new Vector2Int(15, 8);

    private List<MainLight> Lights;

    public IEnumerable<MainLight> ActiveLights
    {
        get
        {
            return Lights.Where(p => p.IsActive());
        }
    }

    public int ActiveLightCount
    {
        get
        {
            return ActiveLights.Count();
        }
    }

    public IEnumerable<MainLight> InactiveLights
    {
        get
        {
            return Lights.Where(p => !p.IsActive());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetupGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupGrid()
    {
        Lights = new List<MainLight>();

        float midX = (Dimensions.x - 1) / 2.0f;
        float midY = (Dimensions.y - 1) / 2.0f;

        for (float x = 0; x < Dimensions.x; x++)
        {
            for (float y = 0; y < Dimensions.y; y++)
            {
                float posX = x - midX;
                float posY = y - midY;

                var newLight = Instantiate(LightPrefab, this.transform);
                newLight.transform.localPosition = new Vector2(posX, posY);

                Lights.Add(newLight.GetComponent<MainLight>());
            }
        }
    }

    public MainLight GetClosestLight(MainLight mainLight)
    {
        var lights = InactiveLights.Where(p => !p.InDarkZone && p != mainLight);

        var closestLight = lights.OrderBy(p => Vector2.Distance(p.transform.position, mainLight.transform.position)).FirstOrDefault();

        return closestLight;
    }

    public void TurnOnElevatorLight()
    {
        var elevator = FindObjectOfType<ElevatorControl>().GetComponent<Collider2D>();

        var elevatorLight = Lights.FirstOrDefault(p => p.GetComponent<Collider2D>().bounds.Intersects(elevator.bounds));

        if (elevatorLight != null)
        {
            elevatorLight.SwitchLight();
        }
    }

    public bool CheckInLight(Vector2 position)
    {
        return ActiveLights.Count(p => p.Collider.bounds.Contains(position)) > 0;
    }
}
