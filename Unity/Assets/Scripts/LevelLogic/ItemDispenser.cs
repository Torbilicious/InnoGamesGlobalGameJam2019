using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDispenser : MonoBehaviour, IPointerDownHandler
{
    public DragableObject TileTemplate;

    public Transform SpawnInto;

    public GameObject Editor;

    /// <summary>
    /// 
    /// </summary>
    public bool Save = true;

    private DispenserData _data;
    private TileData _currentItem;
    private DragableObject _spawnedItem;

    private TileType _editorSelectedType;
    private float _editorSelectedRotation;

    void Start()
    {
        if(_data == null)
        {
            SetDispenserData(new DispenserData());
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !LevelManager.EditorEnabled && _data.InUse)
        {
            _spawnedItem = Instantiate(TileTemplate, gameObject.transform, true);
            _spawnedItem.GetComponent<LevelTileX>().SetTileData(_currentItem, true);
            _spawnedItem.OnDragStopPostProcess.AddListener(DispensedItemStopped);
            _spawnedItem.transform.parent = SpawnInto;
            gameObject.SetActive(false);
        }
        else if(eventData.button == PointerEventData.InputButton.Right && LevelManager.EditorEnabled && this.Save)
        {
            Editor.SetActive(!Editor.activeSelf);
        }
    }

    public DispenserData GetDispenserData()
    {
        return _data;
    }

    public void SetDispenserData(DispenserData data)
    {
        _data = data;

        if (_data.InUse)
        {
            _currentItem = _data.GetNextTileData();
        }
        SynchronizeData();
    }

    private void SynchronizeData()
    {
        //gameObject.SetActive(_data.InUse);

        ApplySpriteFromMimickedItem();

        transform.Find("Tile Drop Settings/Enabled").GetComponent<Toggle>().isOn = _data.InUse;
        transform.Find("Tile Drop Settings/SpawnType").GetComponent<Dropdown>().value = (int)_data.SpawnType;
    }

    private void ApplySpriteFromMimickedItem()
    {
        Sprite sprite = _data.InUse ? TileAttribute.GetSpriteFromTileType(_currentItem.TileType) : null; //TODO: Grafik für deaktivierten Dispenser
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().sprite = sprite;
        }
        else
        {
            GetComponent<Image>().sprite = sprite;
        }
        transform.eulerAngles = new Vector3(0, 0, _currentItem.Rotation);
    }

    public void Reset(bool success)
    {
        if (success)
        {
            _currentItem = _data.GetNextTileData();
            _spawnedItem = null;
            if (_currentItem != null)
            {
                ApplySpriteFromMimickedItem();
            }
        }
        else
        {
            Destroy(_spawnedItem.gameObject);
        }
        gameObject.SetActive(_currentItem != null);
    }

    private void DispensedItemStopped(DragEventArgs args)
    {
        _spawnedItem.OnDragStopPostProcess.RemoveListener(DispensedItemStopped);
        Reset(_spawnedItem.DragSuccessful);
    }

    public void OnEnabledChanged(Toggle toggle)
    {
        _data.InUse = toggle.isOn;
        SynchronizeData();
    }

    public void OnSpawnTypeChanged(Dropdown dropdown)
    {
        _data.SpawnType = (DispenserSpawnType)dropdown.value;
        SynchronizeData();
    }
}
