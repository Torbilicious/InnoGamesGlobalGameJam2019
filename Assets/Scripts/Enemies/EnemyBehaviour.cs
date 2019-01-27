using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    public DropTile startTile;

    public float Speed = 1.0f;

    public float SpeedMax = 2.0f;
    
    private float _currentSpeed = 1.0f;

    private int _currentSprite;

    private DropTile tileDestination;

    public SpriteRenderer RendererRun;
    public SpriteRenderer RendererDust;
    public Animation2D AnimationRun;
    public Animation2D AnimationDust;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        startTile.EnemyOnTile = true;
    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if(GameState.isDead)return;

        if (tileDestination == null) 
        {
            tileDestination = startTile.GetRandomNextTile(startTile) ?? startTile;
        }
        
        _currentSpeed = Speed * Time.deltaTime;

        Vector3 targetPos = tileDestination.transform.position;
        targetPos.z = transform.position.z;

        if(!startTile.IsNextTileValid(tileDestination))
        {
             transform.position = startTile.transform.position;
             tileDestination.EnemyOnTile = false;
             startTile.EnemyOnTile = true;
             tileDestination = startTile;
             return;
        }

        // move to next tile
        if( (targetPos - transform.position).magnitude < 0.01f)
        {
            transform.position = targetPos;

            DropTile nextStartTile = tileDestination;
            tileDestination = tileDestination.GetRandomNextTile(startTile);
            //There is no next tile
            if(tileDestination == null)
            {
                tileDestination = startTile;
            }
            
            startTile.EnemyOnTile = false;
            startTile = nextStartTile;
            startTile.EnemyOnTile = true;
        }
        else
        {
            this.gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPos, _currentSpeed);
        }

        SetAnimation();
    }

    void SetAnimation()
    {
        RendererRun.sprite = AnimationRun.GetNext();
        RendererDust.sprite = AnimationDust.GetNext();
    }
}
