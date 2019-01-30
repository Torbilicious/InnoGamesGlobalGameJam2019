using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    EMPTY,
    STRAIGHT,
    CORNER,
    THREEWAY,
    CROSSROAD,
    DEADEND
}

[Flags]
public enum TileDirection
{
    NONE = 0,
    TOP = 1,
    RIGHT = 2,
    BOTTOM = 4,
    LEFT = 8
}

public static class TileAttribute
{
    private static Dictionary<string, Sprite> _spriteBuffer = new Dictionary<string, Sprite>();

    public static Sprite GetSpriteFromTileType(TileType tileType)
    {
        string key = "";

        switch (tileType)
        {
            case TileType.EMPTY:
                key = "empty";
                break;
            case TileType.STRAIGHT:
                key = "straight";
                break;
            case TileType.CORNER:
                key = "corner";
                break;
            case TileType.THREEWAY:
                key = "three_way";
                break;
            case TileType.CROSSROAD:
                key = "crossroad";
                break;
            case TileType.DEADEND:
                key = "deadend";
                break;
        }
        if (!_spriteBuffer.ContainsKey(key))
        {
            _spriteBuffer.Add(key, Resources.Load<Sprite>("Images/Tiles/" + key));
        }
        return _spriteBuffer[key];
    }

    public static TileDirection GetDirectionsFromTileType(TileType tileType)
    {
        switch(tileType)
        {
            case TileType.EMPTY:
                return TileDirection.NONE;
            case TileType.STRAIGHT:
                return (TileDirection.TOP | TileDirection.BOTTOM);
            case TileType.CORNER:
                return (TileDirection.TOP | TileDirection.LEFT);
            case TileType.THREEWAY:
                return (TileDirection.TOP | TileDirection.BOTTOM | TileDirection.LEFT);
            case TileType.CROSSROAD:
                return (TileDirection.TOP | TileDirection.BOTTOM | TileDirection.LEFT | TileDirection.RIGHT);
            case TileType.DEADEND:
                return (TileDirection.BOTTOM);
        }

        throw new NotImplementedException("Unknown tile type!");
    }
}