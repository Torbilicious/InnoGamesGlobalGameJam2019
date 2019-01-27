using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public bool IsExit;
    public Animation2D EntryAnimation;
    public Animation2D ExitAnimation;
    public SpriteRenderer Renderer;

    void Update()
    {
        if(transform.parent != null)
        {
            transform.parent = null;
        }
        Renderer.sprite = IsExit ? ExitAnimation.GetNext() : EntryAnimation.GetNext();
        transform.Rotate(0, 0, 100.0f * Time.deltaTime);
    }
}
