using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float DragSpeed = 17;
    private const int _mouseButton = 1;

    public Rect WorldBounds = new Rect(-5, -5, 10, 10);

    private Vector3 _velocity;

    private void Update()
    {
        if (!Input.GetMouseButton(_mouseButton)) return;
        transform.position += new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime * -DragSpeed, Input.GetAxis("Mouse Y") * Time.deltaTime * -DragSpeed, 0f);
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, WorldBounds.xMin, WorldBounds.xMax),
            Mathf.Clamp(transform.position.y, WorldBounds.yMin, WorldBounds.yMax),
            transform.position.z);
    }
}
