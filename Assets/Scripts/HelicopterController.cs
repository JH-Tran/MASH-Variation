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

    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider2D;

    private bool isPlayerAlive = true;

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
            isPlayerAlive = false;
            rb2d.velocity = Vector3.zero;
            Debug.Log("Lose");
        }
        else if (collision.gameObject.CompareTag("Hospital"))
        {
            collision.gameObject.GetComponent<Hospital>().RescuedSoldiers(DropSoldiers());
        }
    }
    private void HelicopterSetUp()
    {
        rb2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void PlayerMovement()
    {
        rb2d.velocity = new Vector3(horizontal * speed, vertical * speed);
    }
    public bool IsHelicopterFull()
    {
        return soldiersCarrying >= maxSoldiers;
    }
    public void IncrementSoldierCarryCount()
    {
        soldiersCarrying += 1;
    }
    private int DropSoldiers()
    {
        int currentSoldiers = soldiersCarrying;
        soldiersCarrying = 0;
        return currentSoldiers;
    }
}