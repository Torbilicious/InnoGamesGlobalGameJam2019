using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public bool activated = false;
    public float rotationSpeed = 0.1f;

    private float currentRotation = 0.0f;

    private static float endRotation = 90.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && currentRotation < endRotation) {

            currentRotation += rotationSpeed;

            if(currentRotation >= endRotation) {
                currentRotation = endRotation;
            }

            //transform.Rotate(0, 0, Time.deltaTime * currentRotation, Space.World);
            transform.RotateAround(this.position, this.up, rotationSpeed * Time.deltaTime);
        }
    }
}
