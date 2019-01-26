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
        currentTile = getRandomTile();
        applySpriteFromMimickedTile();
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

    public void reset(ResetCause cause)
    {
        switch (cause)
        {
            case PLACED:
                //TODO maybe rotate TileToSpawn
                currentTile = getRandomTile();
                applySpriteFromMimickedTile();
                break;
        }

        gameObject.SetActive(true);
    }

    private void applySpriteFromMimickedTile()
    {
        GetComponent<SpriteRenderer>().sprite = currentTile.GetComponent<SpriteRenderer>().sprite;
        transform.rotation = currentTile.transform.rotation;
    }

    private GameObject getRandomTile()
    {
        return TilesToSpawn[Random.Range(0, TilesToSpawn.Length)];
    }
}

public enum ResetCause
{
    CANCEL,
    PLACED
}