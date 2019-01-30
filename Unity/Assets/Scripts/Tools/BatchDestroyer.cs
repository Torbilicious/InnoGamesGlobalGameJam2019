using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BatchDestroyer
{

    public static Transform RemoveAllChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            Object.Destroy(child.gameObject);
        }
        return transform;
    }
}

