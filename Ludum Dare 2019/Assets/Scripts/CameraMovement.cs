using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private const float MIN_SIZE = 1.0f;
    private const float MAX_SIZE = 4.0f;

    private new Camera camera;
    private Transform player;

    [Range(MIN_SIZE, MAX_SIZE)]
    public float CameraSize = MIN_SIZE;
    public float ZoomSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
        player = FindObjectOfType<PlayerController>().transform;
        CameraSize = MIN_SIZE;
    }

    // Update is called once per frame
    void Update()
    {
        HandleZoom();

        HandlePosition();
    }

    private void HandleZoom()
    {
        var scrollAxis = Input.GetAxis("Mouse ScrollWheel");
        var sizeIncrement = scrollAxis * Time.deltaTime * ZoomSpeed;
        CameraSize += sizeIncrement;

        CameraSize = Mathf.Clamp(CameraSize, MIN_SIZE, MAX_SIZE);

        camera.orthographicSize = CameraSize;
    }

    private void HandlePosition()
    {
        var zoomFactor = 1 - ((CameraSize - MIN_SIZE) / (MAX_SIZE - MIN_SIZE));

        var newPosition = Vector3.Lerp(Vector2.zero, player.transform.position, zoomFactor);

        //var newPosition = player.transform.position;
        newPosition.z = transform.position.z;
        transform.position = newPosition;
    }
}
