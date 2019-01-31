using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

/// <summary>
/// This class contains information about a tile in a level.
/// </summary>
[DataContract]
public class TileData
{
    /// <summary>
    /// Defines if the tile is in use
    /// </summary>
    [DataMember]
    [DispenserOverride]
    public bool InUse = false;

    /// <summary>
    /// The x-position on the level grid
    /// </summary>
    [DataMember]
    public int PosX;

    /// <summary>
    /// The y-position on the level grid
    /// </summary>
    [DataMember]
    public int PosY;

    /// <summary>
    /// Defines if this tile is a portal
    /// </summary>
    [DataMember]
    public bool IsPortal;

    /// <summary>
    /// Defines if this tile is a portal exit
    /// </summary>
    [DataMember]
    public bool IsPortalExit;

    /// <summary>
    /// The target location of the portal
    /// </summary>
    [DataMember]
    public Vector2 PortalTargetLocation;

    /// <summary>
    /// Reference to the TileData of the portal target
    /// </summary>
    public TileData PortalTarget;

    [DataMember]
    [DispenserOverride]
    public float Rotation;

    [DataMember]
    [DispenserOverride]
    public TileType TileType;

    /// <summary>
    /// Defines if the tile is used a a dispenser for another tile.
    /// </summary>
    public bool IsDispenser;

    public TileData()
    {
        InUse = false;
        IsPortal = false;
        PortalTarget = null;
        Rotation = 0.0f;
        TileType = TileType.EMPTY;
        IsDispenser = false;
    }

    public GameObject Instantiate(GameObject prefab, GameObject grid)
    {
        GameObject obj = Object.Instantiate(prefab);
        obj.transform.parent = grid.transform;
        obj.GetComponent<DragableObject>().enabled = false;
        obj.GetComponent<LevelTileX>().SetTileData(this);
        return obj;
    }
}
