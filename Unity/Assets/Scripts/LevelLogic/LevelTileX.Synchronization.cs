using UnityEngine;
using UnityEngine.UI;

public partial class LevelTileX : MonoBehaviour
{
    private void SynchronizeData()
    {
        SynchronizePortals();
        SynchronizeGameObjectAttributes();
        SynchronizeUI();

        //data update
        _data.PosX = (int)transform.position.x;
        _data.PosY = (int)transform.position.y;
    }

    private void SynchronizePortals()
    {
        //update existing portals
        if ((_data.IsPortal || _data.IsPortalExit) && _data.PortalTarget != null)
        {
            _data.PortalTarget.PortalTargetLocation = new Vector2(transform.position.x, transform.position.y);
        }

        //connect portal
        if (_data.IsPortal)
        {
            LevelTileX portalTarget = FindTileByPosition((int)_data.PortalTargetLocation.x, (int)_data.PortalTargetLocation.y);
            if (portalTarget != null && portalTarget != this)
            {
                TileData backTrace = portalTarget._data.PortalTarget;
                if (backTrace == null || backTrace == _data)
                {
                    portalTarget._data.IsPortalExit = true;
                    portalTarget._data.PortalTargetLocation = new Vector2(transform.position.x, transform.position.y);
                    portalTarget._data.PortalTarget = _data;
                    _data.PortalTarget = portalTarget._data;
                    portalTarget.SynchronizeData();
                }
            }
        }

        //backtrace disconnected portals
        if (!_data.IsPortal && !_data.IsPortalExit && _data.PortalTarget != null)
        {
            LevelTileX portalTarget = FindTileByPosition((int)_data.PortalTargetLocation.x, (int)_data.PortalTargetLocation.y);
            if (portalTarget != null && portalTarget != this)
            {
                TileData backTrace = portalTarget._data.PortalTarget;
                if (backTrace == _data)
                {
                    portalTarget._data.IsPortalExit = false;
                    portalTarget._data.PortalTarget = null;
                    portalTarget.SynchronizeData();
                }
            }
        }

        //invalidate broken portals
        if (_data.IsPortalExit)
        {
            LevelTileX portalTarget = FindTileByPosition((int)_data.PortalTargetLocation.x, (int)_data.PortalTargetLocation.y);
            if (portalTarget != null && portalTarget != this)
            {
                TileData backTrace = portalTarget._data.PortalTarget;
                if (backTrace == null || backTrace != _data || !portalTarget._data.IsPortal)
                {
                    _data.IsPortalExit = false;
                    _data.PortalTarget = null;
                }
            }
        }
    }

    private void SynchronizeGameObjectAttributes()
    {
        //Game object update
        this.name = "LevelTile " + transform.position.x + ":" + transform.position.y;
        transform.Find("LevelTileEditor/Editor/Caption").GetComponent<Text>().text = this.name;

        Portal.gameObject.SetActive(_data.IsPortal || _data.IsPortalExit);
        Portal.IsExit = _data.IsPortalExit;
        
        transform.eulerAngles = new Vector3(0, 0, _data.Rotation);

        SpriteRenderer.sprite = TileAttribute.GetSpriteFromTileType(_data.TileType);
        GameMechanic.Directions = TileAttribute.GetDirectionsFromTileType(_data.TileType);
    }

    private void SynchronizeUI()
    {
        //editor ui update
        transform.Find("LevelTileEditor/Editor/Type/Dropdown").GetComponent<Dropdown>().value = (int)_data.TileType;
        transform.Find("LevelTileEditor/Editor/Rotation/Dropdown").GetComponent<Dropdown>().value = (int)(_data.Rotation / 90.0f);
        transform.Find("LevelTileEditor/Editor/IsPortal").GetComponent<Toggle>().isOn = _data.IsPortal;
        transform.Find("LevelTileEditor/Editor/IsPortal").GetComponent<Toggle>().interactable = !_data.IsPortalExit;
        transform.Find("LevelTileEditor/Editor/IsPortalExit").GetComponent<Toggle>().isOn = _data.IsPortalExit;
        transform.Find("LevelTileEditor/Editor/PortalTarget/Input").GetComponent<InputField>().text = _data.PortalTargetLocation.x + ":" + _data.PortalTargetLocation.y;
    }
}
