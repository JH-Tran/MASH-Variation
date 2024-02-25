using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource audioSource;
    public void Start()
    {
        gameManager = GameObject.Find("Main Camera").GetComponent<GameManager>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void RescuedSoldiers(int soldiersCount)
    {
        gameManager.TotalSoldiersRescued = soldiersCount;
        audioSource.Play();
    }
}
