using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[DataContract]
public class TileData
{
    [DataMember]
    public bool InUse = false;

    [DataMember]
    public int PosX;

    [DataMember]
    public int PosY;

    public TileData()
    {
        InUse = false;
    }

    public TileData(LevelTileX tile)
    {
        InUse = true;
        PosX = (int)tile.transform.position.x;
        PosY = (int)tile.transform.position.y;
    }

    public GameObject Instantiate(GameObject prefab, GameObject grid)
    {
        GameObject obj = Object.Instantiate(prefab);
        obj.transform.parent = grid.transform;
        obj.transform.position = new Vector3(PosX, PosY, 0);
        obj.GetComponent<DragableObject>().DragOnSpawn = false;
        return obj;
    }
}
