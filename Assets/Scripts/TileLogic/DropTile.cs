using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTile : MonoBehaviour
{
    bool isDragging = true;

    private Collider2D _lastCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDragging && Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePos;
        }
        else if(isDragging && !Input.GetMouseButton(0))
        {
            if(_lastCollider != null)
            {
                this.transform.position = _lastCollider.transform.position;
                Destroy(_lastCollider.gameObject);
                isDragging = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(isDragging)
        {
            //Input.GetMouseButton(0)
            
            if(other.CompareTag("Tile"))
            {
                _lastCollider = other;
                
            }
        }
    }
}
