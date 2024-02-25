using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 2.2f;
    private int soldiersCarrying = 0;
    private int maxSoldiers = 3;
    private bool isPlayerAlive = true;

    private Rigidbody2D rb2d;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] Transform spawnPoint;

    public int SolderiersCarrying
    {
        get => soldiersCarrying;
    }

    // Start is called before the first frame update
    void Start()
    {
        HelicopterSetUp();
    }
    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        if (isPlayerAlive)
        {
            PlayerMovement();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            if (isPlayerAlive)
            {
                isPlayerAlive = false;
                rb2d.velocity = Vector3.zero;
                GameObject.Find("Main Camera").GetComponent<GameManager>().LoseState();
                Debug.Log("Lose");
            }
        }
        else if (collision.gameObject.CompareTag("Hospital"))
        {
            collision.gameObject.GetComponent<Hospital>().RescuedSoldiers(DropSoldiers());
        }
    }
    private void HelicopterSetUp()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    public void ResetHelicopter()
    {
        rb2d.velocity = Vector3.zero;
        gameObject.transform.position = spawnPoint.position;
        isPlayerAlive = true;
        soldiersCarrying = 0;
        animator.SetBool("isFull", false);
    }
    private void PlayerMovement()
    {
        rb2d.velocity = new Vector3(horizontal * speed, vertical * speed);
        if (horizontal > 0)
        {
            gameObject.transform.localScale = new Vector3(1,1,1);
        }
        else if (horizontal < 0)
        {
            gameObject.transform.localScale = new Vector3(-1,1,1);
        }
    }
    public bool IsHelicopterFull()
    {
        return soldiersCarrying >= maxSoldiers;
    }
    public void IncrementSoldierCarryCount()
    {
        soldiersCarrying += 1;
        audioSource.Play();
        if (IsHelicopterFull())
        {
            animator.SetBool("isFull", true);
        }
    }
    private int DropSoldiers()
    {
        int currentSoldiers = soldiersCarrying;
        soldiersCarrying = 0;
        animator.SetBool("isFull", false);
        return currentSoldiers;
    }
}