using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItem : MonoBehaviour
{
    public GameObject ItemPrefab; //Platzhalter, Gegenstand der fallen gelassen werden soll

    public float DropOnFearLevel = 0;
    
    public bool IsDropped { get; private set; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropItem(Vector3 vecPos)
    {
        Instantiate(this.ItemPrefab, vecPos, Quaternion.identity);
        this.IsDropped = true;
    }
}
