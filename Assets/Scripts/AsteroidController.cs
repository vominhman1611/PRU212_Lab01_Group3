using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    GameObject scoreTextGO;
    public GameObject ExpliosionGO;

     float speed;
    bool isHit = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;

        //Get the score text UI 
        scoreTextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        //Get the enermy position
        Vector2 position = transform.position;

        //Compute the enermy new position
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        //Update the enemy position
        transform.position = position;

        //this is the bottom-left point of the screen 
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //if the enermy went outside the screen on the bottom, then destroy the enermy
        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect collision with player bullet
        if (collision.tag == "PlayerBulletTag")
        {
            EnemyExplosion();

            if (!isHit)
            {
                isHit = true;
                //add 300 points to the score 
                scoreTextGO.GetComponent<GameScore>().Score += 300;
                StartCoroutine(HitCooldown());
            }
            Destroy(gameObject);
        }else if(collision.tag == "PlayerShipTag")
        {
            EnemyExplosion();

            if (!isHit)
            {
                isHit = true;
                //add 300 points to the score 
                scoreTextGO.GetComponent<GameScore>().Score -= 1000;
                StartCoroutine(HitCooldown());
            }
            Destroy(gameObject);
        }
    }
    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        isHit = false;
    }
    void EnemyExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExpliosionGO);
        explosion.transform.position = transform.position;
    }
}
