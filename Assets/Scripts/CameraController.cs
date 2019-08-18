using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera cam;
    public float speed;
    public float minZoom;
    public float maxZoom;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxisRaw("Mouse X");
            float mouseY = Input.GetAxisRaw("Mouse Y");
            transform.position -= new Vector3(mouseX + mouseY, 0.0f, mouseY - mouseX) * speed * Time.deltaTime;
        }

        if (Input.mouseScrollDelta.y != 0)
        {
            cam.orthographicSize -= Input.mouseScrollDelta.y * Time.deltaTime * speed;

            if (cam.orthographicSize >= minZoom)
            {
                cam.orthographicSize = minZoom;
            }
            else if (cam.orthographicSize <= maxZoom)
            {
                cam.orthographicSize = maxZoom;
            }
        }
    }
}
