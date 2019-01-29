using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTileX : MonoBehaviour
{
    private bool _collidesWithOtherLevelTile = false;
    private Collider2D _lastCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.name = "LevelTile " + transform.position.x + ":" + transform.position.y;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<LevelTileX>() != null &&
            (int)other.gameObject.transform.position.x == (int)this.gameObject.transform.position.x &&
                (int)other.gameObject.transform.position.y == (int)this.gameObject.transform.position.y)
        {
            _collidesWithOtherLevelTile = true;
            _lastCollider = other;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other == _lastCollider)
            _collidesWithOtherLevelTile = false;
    }

    public void OnDragStop(DragEventArgs args)
    {
        GetComponent<DragableObject>().DragSuccessful = !_collidesWithOtherLevelTile;
    }
}
