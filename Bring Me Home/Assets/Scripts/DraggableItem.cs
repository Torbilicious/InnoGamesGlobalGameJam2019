using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;


    private void OnMouseDown()
    {
        var position = gameObject.transform.position;

        screenPoint = Camera.main.WorldToScreenPoint(position);
        offset = position - Camera.main.ScreenToWorldPoint(new Vector3(
                     Input.mousePosition.x,
                     Input.mousePosition.y,
                     screenPoint.z
                 ));
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hoverHit);
//        Debug.Log("Hit: " + (hit && hoverHit.transform.tag.Equals("PlaceableArea")));
    }
}