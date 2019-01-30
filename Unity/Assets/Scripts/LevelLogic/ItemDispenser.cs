using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDispenser : MonoBehaviour, IPointerDownHandler
{
    public DragableObject TileTemplate;

    public TileDropper[] TileSettings;

    public Transform SpawnInto;

    private TileDropper _currentItem;
    private DragableObject _spawnedItem;

    private void Start()
    {
        _currentItem = GetRandomItem();
        ApplySpriteFromMimickedItem();
    }

    void OnMouseDown()
    {
        _spawnedItem = Instantiate(TileTemplate, gameObject.transform, true);
        _spawnedItem.GetComponent<TileDropper>().TileType = _currentItem.TileType;
        _spawnedItem.GetComponent<TileDropper>().Rotation = _currentItem.Rotation;
        _spawnedItem.OnDragStopPostProcess.AddListener(DispensedItemStopped);
        _spawnedItem.transform.parent = SpawnInto;
        gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnMouseDown();
    }

    private TileDropper GetRandomItem()
    {
        return TileSettings[Random.Range(0, TileSettings.Length)];
    }

    private void ApplySpriteFromMimickedItem()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().sprite = TileAttribute.GetSpriteFromTileType(_currentItem.TileType);
        }
        else
        {
            GetComponent<Image>().sprite = TileAttribute.GetSpriteFromTileType(_currentItem.TileType);
        }
        transform.rotation = _currentItem.transform.rotation;
    }

    public void Reset(bool success)
    {
        if (success)
        {
            _currentItem = GetRandomItem();
            ApplySpriteFromMimickedItem();
            _spawnedItem = null;
        }
        else
        {
            Destroy(_spawnedItem.gameObject);
        }
        gameObject.SetActive(true);
    }

    private void DispensedItemStopped(DragEventArgs args)
    {
        _spawnedItem.OnDragStopPostProcess.RemoveListener(DispensedItemStopped);
        Reset(_spawnedItem.DragSuccessful);
    }
}
