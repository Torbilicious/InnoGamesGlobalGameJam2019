using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using static ResetCause;

public class SpawnableItem : MonoBehaviour, IPointerDownHandler
{
    public GameObject[] TilesToSpawn;
    private GameObject currentTile;

    private void Start()
    {
        currentTile = GetRandomTile();
        ApplySpriteFromMimickedTile();
    }

    private void OnMouseDown()
    {
        var tileToSpawn = currentTile;

        var tile = Instantiate(tileToSpawn, gameObject.transform, true);
        tile.transform.parent = null;
        tile.GetComponent<DropTile>().SpawnableItem = this;
        tile.GetComponent<DropTile>().isDragging = true;

        gameObject.SetActive(false);
    }

    public void Reset(ResetCause cause)
    {
        switch (cause)
        {
            case PLACED:
                //TODO maybe rotate TileToSpawn
                currentTile = GetRandomTile();
                ApplySpriteFromMimickedTile();
                break;
        }

        gameObject.SetActive(true);
    }

    private void ApplySpriteFromMimickedTile()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().sprite = currentTile.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            GetComponent<Image>().sprite = currentTile.GetComponent<SpriteRenderer>().sprite;
        }
        transform.rotation = currentTile.transform.rotation;
    }

    private GameObject GetRandomTile()
    {
        return TilesToSpawn[Random.Range(0, TilesToSpawn.Length)];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnMouseDown();
    }
}

public enum ResetCause
{
    CANCEL,
    PLACED
}