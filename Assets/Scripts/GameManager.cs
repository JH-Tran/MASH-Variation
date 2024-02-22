using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private int totalSoldiersRescued = 0;
    private string totalSoldiersString = "Soldiers Rescued: ";
    private string soldiersHelicopterString = "Soldiers In Helicopter: ";
    private List<Transform> soldiersTransform = new List<Transform>();
    private int maxSoldiers = 1;
    private bool gameEnd = false;

    [SerializeField] HelicopterController controller;
    [SerializeField] TextMeshProUGUI totalRescuedText;
    [SerializeField] TextMeshProUGUI soldiersHelicopterText;

    [SerializeField] GameObject soldiersPrefab;
    [SerializeField] Transform injuredSoldiers;
    [SerializeField] GameObject soldiers;
    
    public int TotalSoldiersRescued
    {
        get => totalSoldiersRescued;
        set
        {
            totalSoldiersRescued += value;
        }
    }
    private void Start()
    {
        foreach (Transform t in injuredSoldiers)
        {
            soldiersTransform.Add(t);
        }
        maxSoldiers = soldiersTransform.Count;
        SpawnSoldiers();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        totalRescuedText.text = totalSoldiersString + totalSoldiersRescued;
        soldiersHelicopterText.text = soldiersHelicopterString + controller.SolderiersCarrying;
        if (totalSoldiersRescued >= maxSoldiers && gameEnd != true)
        {
            gameEnd = true;
            WinState();
        }
    }
    private void ResetGame()
    {
        ResartSoldiers();
        SpawnSoldiers();
        totalSoldiersRescued = 0;
        GameObject.FindWithTag("Player").GetComponent<HelicopterController>().ResetHelicopter();
        gameEnd = false;
    }
    private void SpawnSoldiers()
    {
        foreach (Transform s in soldiersTransform)
        {
            GameObject soldier = Instantiate(soldiersPrefab, s.transform.position, Quaternion.identity);
            soldier.transform.SetParent(soldiers.transform);
        }
    }
    private void ResartSoldiers()
    {
        foreach (Transform soldier in soldiers.transform)
        {
            Destroy(soldier.gameObject);
        }
    }
    private void WinState()
    {
        Debug.Log("Win");
    }
    public void LoseState()
    {
        Debug.Log("Lose");
    }
}
