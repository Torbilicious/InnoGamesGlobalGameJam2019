using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class LevelTileX : MonoBehaviour
{
    private const string nameIdentifier = "LevelTile ";

    private bool _collidesWithOtherLevelTile = false;
    private Collider2D _lastCollider;
    private bool _mouseOver = false;

    private TileData _data;
    private float _synchronizationTimer = 1.0f;

    public GameObject Editor;

    public Portal Portal;

    public LevelTileX()
    {
        _data = new TileData();
    }

    // Start is called before the first frame update
    void Start()
    {
        Editor.SetActive(false);
    }

    void Update()
    {
        //Editor stuff
        if(Input.GetMouseButtonDown(1) && _mouseOver)
        {
            Editor.SetActive(!Editor.activeSelf);
        }

        if ((_synchronizationTimer -= Time.deltaTime) < 0)
        {
            SynchronizeData();
            _synchronizationTimer = 60.0f;
        }
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

    void OnMouseOver()
    {
        _mouseOver = true;
    }

    void OnMouseExit()
    {
        _mouseOver = false;
    }

    private LevelTileX FindTileByPosition(int x, int y)
    {
        return GameObject.Find(nameIdentifier + x + ":" + y)?.GetComponent<LevelTileX>();
    }

    public void SetTileData(TileData data)
    {
        _data = data;
        transform.position = new Vector3(_data.PosX, _data.PosY, 0);
        SynchronizeData();
    }

    public TileData GetTileData()
    {
        return _data;
    }

    public void OnDragStop(DragEventArgs args)
    {
        if (_collidesWithOtherLevelTile &&
            ((int)_lastCollider.gameObject.transform.position.x != (int)this.gameObject.transform.position.x ||
            (int)_lastCollider.gameObject.transform.position.y != (int)this.gameObject.transform.position.y))
        {
            _collidesWithOtherLevelTile = false;
        }

        GetComponent<DragableObject>().DragSuccessful = !_collidesWithOtherLevelTile;

        if (!_collidesWithOtherLevelTile)
        {
            _data.PosX = (int)transform.position.x;
            _data.PosY = (int)transform.position.y;
            GetComponent<DragableObject>().enabled = false;
            SynchronizeData();
        }
    }


}
