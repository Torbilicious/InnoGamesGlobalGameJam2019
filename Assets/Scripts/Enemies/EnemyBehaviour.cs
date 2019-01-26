using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public DropTile startTile;

    public float FearLevel = 0;

    public float FearLevelMax = 100;

    public float Speed = 1.0f;

    public float SpeedMax = 2.0f;
    
    private float _currentSpeed = 1.0f;

    private int _currentSprite;

    private DropTile tileDestination;

    public SpriteRenderer RendererRun;
    public SpriteRenderer RendererDust;
    public Animation2D AnimationRun;
    public Animation2D AnimationDust;

    private bool firstTileSet = false;

//    private TrapItem _trapItem;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {

        //transform.position = startTile.transform.position + new Vector3(-0.25f, 0.25f, transform.position.z);
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        //SetAnimation();
        if(!firstTileSet)
        {
            tileDestination = startTile.GetRandomNextTile(startTile);
            if (tileDestination != null)
            {
                firstTileSet = true;
                tileDestination.CanRotate = false;
            }
            else
            {
                return;
            }
        }

        _currentSpeed = (Speed + SpeedMax * (FearLevel / FearLevelMax)) * Time.deltaTime;

        Vector3 targetPos = tileDestination.transform.position;// + new Vector3(-0.25f, 0.25f, 0.0f);
        targetPos.z = transform.position.z;

        // move to next tile
        if( (targetPos - transform.position).magnitude < 0.01f)
        {
            transform.position = targetPos;
            tileDestination.AddModifiers(this);
            DropTile nextStartTile = tileDestination;
            tileDestination = tileDestination.GetRandomNextTile(startTile);
            if(tileDestination == null)
            {
                Die();
                return;
            }
            
            startTile = nextStartTile;
            nextStartTile.CanRotate = false;
        }
        else
        {
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPos, _currentSpeed);
        }

        SetAnimation();

        if(this.FearLevel >= this.FearLevelMax)
        {
            //TODO: Was soll passieren, wenn FearLevel voll ist??
            //_trapItem.dosomething();
        }

        //TODO: Triggers for traps
    }

    void SetAnimation()
    {
        //TODO: Detect type of current movement, find objects and change Move animation (climb, run, etc.)
        RendererRun.sprite = AnimationRun.GetNext();
        RendererDust.sprite = AnimationDust.GetNext();
    }

    public void Die()
    {
        //TODO: Game Over
        Destroy(this.gameObject);
    }

    public void TeleportTo(DropTile tile)
    {
        Vector3 target = tile.transform.position;
        target.z = transform.position.z;
        transform.position = target;
        tileDestination = tile;
    }
}
