using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Dictionary<Vector2, DropTile> _droppedTiles = new Dictionary<Vector2, DropTile>();
    public int nextLevel;

    public AudioSource bgSound;

    public AudioClip dieSound;

    private bool diePlayed = false; 

    // Start is called before the first frame update
    void Start()
    {
        GameState.Reset();
        GameState.nexLevel = nextLevel;
        _droppedTiles = new Dictionary<Vector2, DropTile>();
        diePlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(bgSound)
        {
            if(GameState.isDead) {
                bgSound.pitch = 0.6f;
                if(!diePlayed)
                {
                    AudioSource.PlayClipAtPoint(dieSound,new Vector3(0.0f, 0.0f, -6.0f), 6.5f);
                    diePlayed = true;
                }
            } else {
                bgSound.pitch = 1.0f;
            }
        }
    }
}
