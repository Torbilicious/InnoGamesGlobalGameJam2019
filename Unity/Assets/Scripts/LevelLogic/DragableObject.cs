/*
 * Attaching this script to an object makes is dragable by mouse
 */
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public struct DragEventArgs
{
    public Vector3 DragStartPosition;
    public Vector3 DragStopPosition;

    public DragEventArgs(Vector3 start, Vector3 stop)
    {
        this.DragStartPosition = start;
        this.DragStopPosition = stop;
    }
}
//This class is required for some retarded reason, using UnityEvent<xxx> directly will hide the event from the editor
[Serializable]
public class UnityEventDrag: UnityEvent<DragEventArgs> { }

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DragableObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool IsDragging = false;

    public bool DragWithMouse = true;
    public bool DragWithPointer = false;
    public bool DragOnSpawn = false;

    public UnityEventDrag OnDragStart;
    public UnityEventDrag OnDragStop;

    public UnityEventDrag OnDragStopPostProcess;

    public bool DragSuccessful { get; set; }

    private Vector3 _start;

    void Start()
    {
        if(DragOnSpawn)
        {
            IsDragging = true;
        }
    }

    void Update()
    {
        if (IsDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
    }

    private void StartDrag()
    {
        _start = this.transform.position;
        IsDragging = true;
        OnDragStart.Invoke(new DragEventArgs(this.transform.position, this.transform.position));
    }

    private void StopDrag()
    {
        IsDragging = false;
        OnDragStop.Invoke(new DragEventArgs(_start, this.transform.position));
        OnDragStopPostProcess.Invoke(new DragEventArgs(_start, this.transform.position));
    }

    private void OnMouseDown()
    {
        if (DragWithMouse)
            StartDrag();
    }

    private void OnMouseUp()
    {
        if (DragWithMouse)
            StopDrag();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (DragWithPointer)
            StartDrag();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (DragWithPointer)
            StopDrag();
    }
}
