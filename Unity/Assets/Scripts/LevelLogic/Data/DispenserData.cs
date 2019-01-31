using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

/// <summary>
/// This class contains informations about a dispenser that is used to drop new tiles into the level.
/// </summary>
[DataContract]
public class DispenserData
{
    [DataMember]
    public bool InUse;

    [DataMember]
    public TileData[] Stack;

    [DataMember]
    public DispenserSpawnType SpawnType;

    [DataMember]
    public bool Repeat;

    private int _position;

    public DispenserData()
    {
        this.InUse = true;
        this.Stack = new TileData[1] { new TileData() };
        this.SpawnType = DispenserSpawnType.RANDOM;
        this.Repeat = true;
        _position = 0;
    }

    public TileData GetNextTileData()
    {
        if(_position >= Stack.Length)
        {
            if(Repeat && Stack.Length != 0)
            {
                _position = 0;
            }
            else
            {
                return null;
            }
        }
        switch(this.SpawnType)
        {
            case DispenserSpawnType.RANDOM:
                return Stack[UnityEngine.Random.Range(0, Stack.Length)];
            case DispenserSpawnType.SEQUENTIAL:
                return Stack[_position++];
            default:
                throw new NotImplementedException("Unknown Dispenser Spawn Type!");
        }
    }
}

public enum DispenserSpawnType
{
    RANDOM,
    SEQUENTIAL
}

public class DispenserOverride : Attribute
{
}