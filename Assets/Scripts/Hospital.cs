using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : MonoBehaviour
{
    private int totalPoints = 0;

    public void RescuedSoldiers(int soldiersCount)
    {
        totalPoints += soldiersCount;
        Debug.Log(totalPoints);
    }

}
