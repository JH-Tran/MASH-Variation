using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehaviour : MonoBehaviour
{
    private GameObject player;
    private Vector3 playerPointPos;
    private float tankSpeed = 1.7f;

    [SerializeField] GameObject startingPoint;
    [SerializeField] GameObject bulletStartingPoint;
    [SerializeField] GameObject bulletPrefab;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.position = startingPoint.transform.position;
        ShootAtPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        playerPointPos = new Vector3(player.transform.position.x, transform.position.y, transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, playerPointPos, tankSpeed * Time.deltaTime);
    }
    public void RestartTank()
    {
        gameObject.transform.position = startingPoint.transform.position;
        ShootAtPlayer();
    }
    public void ShootAtPlayer()
    {
        if (GameObject.FindGameObjectsWithTag("Bullet").Length <= 1)
        {
            GameObject bulletObj = Instantiate(bulletPrefab, bulletStartingPoint.transform.position, Quaternion.identity);
            bulletObj.GetComponent<BulletBehaviour>().SetTank(this);
        }

    }
}
