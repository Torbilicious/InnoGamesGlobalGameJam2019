using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler
{
    //public GameObject[] TilesToSpawn;
   // private GameObject currentTile;

    private void Start()
    {
        //currentTile = getRandomTile();
        //applySpriteFromMimickedTile();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {

        transform.position = Input.mousePosition;
        //throw new System.NotImplementedException();
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        /*transform.localPosition = Vector3.zero;
        var tileToSpawn = currentTile;

        var tile = Instantiate(tileToSpawn, gameObject.transform, true);
        Debug.Log(tile);*/
        //tile.transform.parent = null;
        //tile.GetComponent<DropTile>().SpawnableItem = this;

        //gameObject.active = false;
        //throw new System.NotImplementedException();
    }
   /* private GameObject getRandomTile()
    {
        return TilesToSpawn[Random.Range(0, TilesToSpawn.Length)];
    }*/


}
