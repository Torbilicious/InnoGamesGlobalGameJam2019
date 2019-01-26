using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public bool activated = false;
    public float rotationSpeed = 0.1f;

    private float currentRotation = 0.0f;

    private static float endRotation = 90.0f;
    private Vector3 rotationPoint;

    // Start is called before the first frame update
    void Start()
    {
        rotationPoint = transform.position - new Vector3(0.6f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
/*         if (transform.parent.gameObject.activated && currentRotation < endRotation) {
            rotationSpeed *= 1.1f;
            currentRotation += Mathf.Abs(rotationSpeed);
            transform.Rotate(0.0f, 0.0f, -rotationSpeed);
}*/
    }
}
