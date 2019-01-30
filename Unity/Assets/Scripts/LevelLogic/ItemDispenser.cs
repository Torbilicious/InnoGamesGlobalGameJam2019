using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDispenser : MonoBehaviour, IPointerDownHandler
{
    public DragableObject[] ItemsToSpawn;

    public Transform SpawnInto;

    private DragableObject _currentItem;
    private DragableObject _spawnedItem;

    private void Start()
    {
        _currentItem = GetRandomItem();
        ApplySpriteFromMimickedItem();
    }

    void OnMouseDown()
    {
        _spawnedItem = Instantiate(_currentItem, gameObject.transform, true);
        _spawnedItem.OnDragStopPostProcess.AddListener(DispensedItemStopped);
        _spawnedItem.transform.parent = SpawnInto;
        gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnMouseDown();
    }

    private DragableObject GetRandomItem()
    {
        return ItemsToSpawn[Random.Range(0, ItemsToSpawn.Length)];
    }

    private void ApplySpriteFromMimickedItem()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().sprite = _currentItem.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            GetComponent<Image>().sprite = _currentItem.GetComponent<SpriteRenderer>().sprite;
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
