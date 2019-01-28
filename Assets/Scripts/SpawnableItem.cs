using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static ResetCause;

public class SpawnableItem : MonoBehaviour
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
                currentTile = GetRandomTile();
                ApplySpriteFromMimickedTile();
                break;
        }

        gameObject.SetActive(true);
    }

    private void ApplySpriteFromMimickedTile()
    {
        GetComponent<SpriteRenderer>().sprite = currentTile.GetComponent<SpriteRenderer>().sprite;
        transform.rotation = currentTile.transform.rotation;
    }

    private GameObject GetRandomTile()
    {
        return TilesToSpawn[Random.Range(0, TilesToSpawn.Length)];
    }
}

public enum ResetCause
{
    CANCEL,
    PLACED
}