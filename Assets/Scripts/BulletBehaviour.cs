using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private TankBehaviour tank;
    private float bulletSpeed = 3.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * bulletSpeed;

        float rotation = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    public void Update()
    {
        Physics.IgnoreLayerCollision(7, 6);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Tree"))
        {
            DestroyBullet();
        }
        else if (collision.gameObject.CompareTag("Environment"))
        {
            DestroyBullet();
        }
    }
    private void DestroyBullet()
    {
        try
        {
            tank.ShootAtPlayer();
            Destroy(gameObject);
        }
        catch
        {
            Debug.Log("Bullet Missing Tank");
        }
    }
    public void SetTank(TankBehaviour tank)
    {
        this.tank = tank;
    }
}
