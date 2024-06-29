using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    public GameObject EnemyBulletGO;
    //public GameObject playerGO;
    //public float distanceBetween = 5f;

    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        /*distance = Vector2.Distance(transform.position, playerGO.transform.position);
        if(distance < distanceBetween)
        {*/
            Invoke("FireEnemyBullet", 1f);
        /*}*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FireEnemyBullet()
    {
        GameObject playerShip = GameObject.Find("PlayerGO");
        if (playerShip != null) 
        {
            
            GameObject bullet = (GameObject)Instantiate(EnemyBulletGO);

            //set the bullet's initial position
            bullet.transform.position = transform.position;
             
            //compute the bullet's direction toward the player's ship
            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            //set the bullet;s direc tion 
            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
            
        }
    }
}
 