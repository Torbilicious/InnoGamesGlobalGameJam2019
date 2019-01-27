using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLizard : MonoBehaviour
{
    uint moveCounter = 0;
    float randRotScale = 1.0f;
    public bool forceShake = false;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();
        randRotScale = (float)rnd.NextDouble() + 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameState.isDead || forceShake) 
        {
            float rot = (float)Mathf.Sin((float)moveCounter * 0.2f) * randRotScale;
            transform.Rotate(0.0f, 0.0f, rot);
            ++moveCounter;
        }
    }
}
