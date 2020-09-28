using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Vector3 wektor;
    Vector3 DragOrigin;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f && !GameObject.Find("Canvas").GetComponent<Generator>().PreventFromZoom) // forward
        {
            this.GetComponent<Camera>().orthographicSize -= 4*Input.GetAxis("Mouse ScrollWheel");
        }

        if (Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(1))
        {
            DragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(2) && !Input.GetMouseButton(1)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - DragOrigin);
        Vector3 move = new Vector3(pos.x * -10, pos.y * -10,0);

        transform.Translate(move, Space.World);


    }
}
