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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        _currentSpeed = (Speed + SpeedMax * (FearLevel / FearLevelMax)) * Time.deltaTime;
        this.gameObject.transform.position = Vector3.MoveTowards(transform.position, SpawnPoint.transform.position, _currentSpeed);

        //Check if Waypoint is reached
        Vector3 vecPosDiff = this.gameObject.transform.position - SpawnPoint.transform.position;
        if (Math.Abs(vecPosDiff.x) < 0.1f && Math.Abs(vecPosDiff.y) < 0.1f && Math.Abs(vecPosDiff.z) < 0.1f)
        {
            if (this.SpawnPoint.HasWayPoint())
            {
                this.SpawnPoint = this.SpawnPoint.GetNextWayPoint();
            }
        }

        //TODO: Detect type of current movement, find objects and change Move animation (climb, run, etc.)

        //TODO: Triggers for traps
    }
}
