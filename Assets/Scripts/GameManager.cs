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
    //Gamestate
    private bool gameEnd = false;
    private bool gamePause = false;

    [SerializeField] HelicopterController controller;
    [SerializeField] TextMeshProUGUI totalRescuedText;
    [SerializeField] TextMeshProUGUI soldiersHelicopterText;
    [SerializeField] GameObject gameStateScreen;
    [SerializeField] GameObject pauseMenuScreen;
    [SerializeField] TextMeshProUGUI winLoseText;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;
    [SerializeField] AudioSource audioSource;

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
    private void Awake()
    {
        gameStateScreen.SetActive(false);
        pauseMenuScreen.SetActive(false);
    }
    private void Start()
    {
        foreach (Transform t in injuredSoldiers)
        {
            soldiersTransform.Add(t);
        }
        maxSoldiers = soldiersTransform.Count;
        SpawnSoldiers();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
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
    private void PauseGame()
    {
        if (!gamePause)
        {
            pauseMenuScreen.SetActive(true);
            Time.timeScale = 0; 
            gamePause = true;
        }
        else if (gamePause)
        {
            pauseMenuScreen.SetActive(false);
            Time.timeScale = 1f;
            gamePause = false;
        }
    }
    private void ResetGame()
    {
        ResartSoldiers();
        SpawnSoldiers();
        totalSoldiersRescued = 0;
        GameObject.FindWithTag("Player").GetComponent<HelicopterController>().ResetHelicopter();
        GameObject bullet = GameObject.FindGameObjectWithTag("Bullet");
        if (bullet != null)
        {
            Destroy(bullet);
        }
        GameObject.FindWithTag("Tank").GetComponent<TankBehaviour>().RestartTank();
        gameStateScreen.SetActive(false);
        pauseMenuScreen.SetActive(false);
        gameEnd = false;
        Time.timeScale = 1f;
        gamePause = false;
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
        gameStateScreen.SetActive(true);
        winLoseText.text = "You Rescued Everyone!";
        audioSource.clip = winSound;
        audioSource.Play();
    }
    public void LoseState()
    {
        gameStateScreen.SetActive(true);
        winLoseText.text = "Game over!";
        audioSource.clip = loseSound;
        audioSource.Play();
    }
}
