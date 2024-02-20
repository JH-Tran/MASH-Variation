using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredSoldier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            try
            {
                if (!collision.gameObject.GetComponent<HelicopterController>().IsHelicopterFull())
                {
                    collision.gameObject.GetComponent<HelicopterController>().IncrementSoldierCarryCount();
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Helicopter full");
                }
            }
            catch (Exception e)
            { 
                Debug.LogError("Injured Soldier: " + e);
            }
        }
    }
}
