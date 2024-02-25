using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehaviour : MonoBehaviour
{

    [SerializeField] GameObject startingPoint;
    [SerializeField] GameObject bulletStartingPoint;
    [SerializeField] GameObject bulletPrefab;


    // Start is called before the first frame update
    void Start()
    {
        ShootAtPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartTank()
    {

    }
    public void ShootAtPlayer()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, bulletStartingPoint.transform.position, Quaternion.identity);
        bulletObj.GetComponent<BulletBehaviour>().SetTank(this);
    }
}
