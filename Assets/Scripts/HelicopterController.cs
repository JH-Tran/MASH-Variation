using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float speed = 1.0f;
    private int soldiersCarrying = 0;
    private int maxSoldiers = 3;

    private Rigidbody2D rb2d;
    private BoxCollider2D boxCollider2D;

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
        PlayerMovement();
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
}