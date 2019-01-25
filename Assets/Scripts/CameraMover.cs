using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    private const int mouseButton = 1;
 
    void Update()
    {
        if (Input.GetMouseButtonDown(mouseButton))
        {
            dragOrigin = Input.mousePosition;
            return;
        }
 
        if (!Input.GetMouseButton(mouseButton)) return;
 
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);
 
        transform.Translate(move, Space.World);  
    }
}
