using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : MonoBehaviour
{
    private GameManager gameManager;
    public void Start()
    {
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
    }
    public void RescuedSoldiers(int soldiersCount)
    {
        gameManager.TotalSoldiersRescued = soldiersCount;
    }
}
