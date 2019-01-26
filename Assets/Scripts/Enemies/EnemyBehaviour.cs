﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public DropTile startTile;

    public EnemyItem[] Items;

    public float FearLevel = 0;

    public float FearLevelMax = 100;

    public float Speed = 1.0f;

    public float SpeedMax = 2.0f;
    
    private float _currentSpeed = 1.0f;

    private int _currentSprite;

    private LevelTile tileDestination;

    public Animation2D AnimationRun;
    public Animation2D AnimationClimb;

    private SpriteRenderer _spr;
    private Animation2D _animationSelected;

//    private TrapItem _trapItem;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        _animationSelected = AnimationRun;
        transform.position = startTile.transform.position + new Vector3(-0.25f, 0.25f, 0.0f);
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if(tileDestination == null) 
        {
            // get next tile   
           
        } else { // check distance

        }

        //Item Drops
        foreach(EnemyItem item in Items)
		{
			if(item.DropOnFearLevel >= this.FearLevel && !item.IsDropped)
			{
                item.DropItem(this.gameObject.transform.position);
			}
		}

        //Room Movement
        /*
        if (this.SpawnPoint != null)
        {
            _currentSpeed = (Speed + SpeedMax * (FearLevel / FearLevelMax)) * Time.deltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, SpawnPoint.transform.position, _currentSpeed);
        }*/

        SetAnimation();

        if(this.FearLevel >= this.FearLevelMax)
        {
            //TODO: Was soll passieren, wenn FearLevel voll ist??
            //_trapItem.dosomething();
        }

        //TODO: Triggers for traps
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Waypoint Update
        /*
        if (other.CompareTag("WayPoint") && this.SpawnPoint.HasWayPoint())
        {
            this.SpawnPoint = this.SpawnPoint.GetNextWayPoint();
        }*/

        //Trap handler
        if(other.CompareTag("Trap"))
        {
            /*TrapItem trapItem = other.gameObject.GetComponent<TrapItem>();
            if(trapItem == null)
            {
                Debug.Log("Trap hat keine Item-Funktion zugewiesen bekommen");
            }
            this.FearLevel += trapItem.Fear;
            _trapItem = trapItem;
            //activate animation
            */
        }
    }

    void SetAnimation()
    {
        //TODO: Detect type of current movement, find objects and change Move animation (climb, run, etc.)
        _spr.sprite = _animationSelected.GetNext();
    }
}
