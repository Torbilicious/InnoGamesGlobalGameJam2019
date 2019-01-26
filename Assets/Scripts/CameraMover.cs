using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float DragSpeed = 2;
    private Vector3 _dragOrigin;
    private const int _mouseButton = 1;

    private Vector3 _velocity;
 
    void Update()
    {
        if (!Input.GetMouseButton(_mouseButton))
        {
            return;
        }
        else if (Input.GetMouseButtonDown(_mouseButton))
        {
            _dragOrigin = Input.mousePosition;
        }
        else
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - _dragOrigin);
            _dragOrigin = Input.mousePosition;
            Vector3 move = Vector3.SmoothDamp(pos, new Vector3(pos.x * -DragSpeed * 200, pos.y * -DragSpeed * 200, 0), ref _velocity, 0.5f);
            transform.Translate(move, Space.World);
        }
    }
}
