using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTile : MonoBehaviour
{
    private enum Portals
    {
        NONE,
        ENTRY,
        EXIT
    }

    public bool IsBlocked = false;

    public float dropSoundVolume = 2.5f;

    public GameObject Block;

    public AudioClip dropSound;

    public float SpeedModifier = 1.0f;

    public LevelTile PortalTo;

    private Portals _portal;

    void Start()
    {
        //Be sure that Tile is snapped to Grid
        this.transform.position = new Vector3(
            (float)Math.Round(this.transform.position.x),
            (float)Math.Round(this.transform.position.y),
            (float)Math.Round(this.transform.position.z));

        if(PortalTo != null && _portal != Portals.EXIT)
        {
            _portal = Portals.ENTRY;
            PortalTo._portal = Portals.EXIT;
            PortalTo.PortalTo = this;
        }
        else
        {
            _portal = Portals.NONE;
        }
    }

    void Update()
    {
        Block.SetActive(IsBlocked);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    public void HandleTileSet()
    {
        AudioSource.PlayClipAtPoint(dropSound, transform.position, dropSoundVolume);
    }

    public bool IsPortalExit()
    {
        return _portal == Portals.EXIT;
    }
}
