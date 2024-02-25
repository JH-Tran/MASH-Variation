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
    private List<Transform> treeTransform = new List<Transform>();
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
    [SerializeField] Transform injuredSoldiersParent;
    [SerializeField] GameObject soldiers;

    [SerializeField] GameObject treePrefab;
    [SerializeField] Transform treesTranformParent;
    [SerializeField] GameObject trees;
    private int maxTrees = 3;

    
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
        foreach (Transform t in injuredSoldiersParent)
        {
            soldiersTransform.Add(t);
        }
        foreach (Transform t in treesTranformParent)
        {
            treeTransform.Add(t);
        }
        maxSoldiers = soldiersTransform.Count;
        SpawnSoldiers();
        SpawnTrees();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gameEnd)
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
        GameObject bullet = GameObject.FindGameObjectWithTag("Bullet");
        if (bullet != null)
        {
            Destroy(bullet);
        }
        RestartSoldiers();
        RestartTrees();
        SpawnSoldiers();
        SpawnTrees();
        totalSoldiersRescued = 0;
        GameObject.FindWithTag("Player").GetComponent<HelicopterController>().ResetHelicopter();
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
    private void SpawnTrees()
    {
        for (int i = 0; i < maxTrees; i++)
        {
            int number;
            GameObject tree;
            bool pass = false;

            while (!pass)
            {
                number = Random.Range(0, 9);
                Transform tempTree = treeTransform[number];
                bool uniqueTransform = true;
                foreach (Transform t in trees.transform)
                {
                    if (tempTree.position == t.position)
                    {
                        uniqueTransform = false;
                        break;
                    }
                }
                if (uniqueTransform)
                {
                    tree = Instantiate(treePrefab, treeTransform[number].transform.position, Quaternion.identity);
                    tree.transform.SetParent(trees.transform);
                    pass = true;
                }
            }
        }
    }
    public void SpawnTree()
    {
        int number;
        GameObject tree;
        bool pass = false;

        while (!pass)
        {
            number = Random.Range(0, treeTransform.Count-1);
            Transform tempTree = treeTransform[number];
            bool uniqueTransform = true;
            foreach (Transform t in trees.transform)
            {
                if (tempTree.position == t.position)
                {
                    uniqueTransform = false;
                    break;
                }
            }
            if (uniqueTransform)
            {
                tree = Instantiate(treePrefab, treeTransform[number].transform.position, Quaternion.identity);
                tree.transform.SetParent(trees.transform);
                pass = true;
            }
        }
    }
    private void RestartSoldiers()
    {
        foreach (Transform soldier in soldiers.transform)
        {
            Destroy(soldier.gameObject);
        }
    }
    private void RestartTrees()
    {
        foreach (Transform tree in trees.transform)
        {
            Destroy(tree.gameObject);
        }
    }
    private void WinState()
    {
        gameStateScreen.SetActive(true);
        gameEnd = true;
        winLoseText.text = "You Rescued Everyone!";
        audioSource.clip = winSound;
        audioSource.Play();
        Time.timeScale = 0f;
    }
    public void LoseState()
    {
        gameStateScreen.SetActive(true);
        gameEnd = true;
        winLoseText.text = "Game over!";
        audioSource.clip = loseSound;
        audioSource.Play();
        Time.timeScale = 0f;
    }
}
