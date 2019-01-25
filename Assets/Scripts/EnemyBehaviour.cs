using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float FearLevel = 0;

    public float FearLevelMax = 100;

    public EnemyItem[] Items;

    public WayPoint SpawnPoint;

    public float Speed = 1.0f;

    public float SpeedMax = 2.0f;
    
    private float _currentSpeed = 1.0f;

    private int _currentSprite;

    public Animation2D AnimationRun;
    public Animation2D AnimationClimb;

    private SpriteRenderer _spr;
    private Animation2D _animationSelected;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        _spr = GetComponent<SpriteRenderer>();
        _animationSelected = AnimationRun;
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        //Item Drops
        foreach(EnemyItem item in Items)
		{
			if(item.DropOnFearLevel >= this.FearLevel && !item.IsDropped)
			{
                item.DropItem(this.gameObject.transform.position);
			}
		}

        //Room Movement
        if (this.SpawnPoint != null)
        {
            _currentSpeed = (Speed + SpeedMax * (FearLevel / FearLevelMax)) * Time.deltaTime;
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, SpawnPoint.transform.position, _currentSpeed);
        }

        SetAnimation();

        //TODO: Triggers for traps
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("WayPoint") && this.SpawnPoint.HasWayPoint())
        {
            this.SpawnPoint = this.SpawnPoint.GetNextWayPoint();
        }
    }

    void SetAnimation()
    {
        //TODO: Detect type of current movement, find objects and change Move animation (climb, run, etc.)
        _spr.sprite = _animationSelected.GetNext();
    }
}
