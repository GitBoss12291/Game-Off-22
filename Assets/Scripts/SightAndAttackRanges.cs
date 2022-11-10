using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightAndAttackRanges : MonoBehaviour
{
    [System.NonSerialized]
    public bool isTriggered;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")) 
        {
            isTriggered = true;
        } 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            isTriggered = false;
        }
    }
}
