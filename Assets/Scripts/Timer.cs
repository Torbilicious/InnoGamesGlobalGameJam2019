using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timer;
    private float timerStart;

    // Start is called before the first frame update
    void Start()
    {
        timerStart = timer;
    }

    void Update()
    {
        if(!GameState.isDead)
        {
            if(timer > 0.0f)
            {
                timer -= Time.deltaTime;
            }else{
                timer = 0.0f;
                GameObject.Find("PlayerObject").gameObject.GetComponent<PlayerBehaviour>().Die();
            }

            float relativeTime = timer / timerStart;
            float rotDir = (((int)(timer) % 2) * 2.0f) - 1.0f;
            Debug.Log(rotDir);

            transform.localScale = new Vector3(relativeTime, relativeTime, 1.0f);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotDir * 25.0f);
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f, relativeTime, relativeTime));

            Debug.Log(relativeTime);
            Debug.Log(timer);
        }
    }
}
