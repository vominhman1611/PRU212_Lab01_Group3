using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarCoinController : MonoBehaviour
{
    GameObject scoreTextGO;
    public AudioClip coinSound;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.5f;

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
        if (collision.tag == "PlayerShipTag")
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
            scoreTextGO.GetComponent<GameScore>().Score += 1000;
            Destroy(gameObject);
        }
    }
}
