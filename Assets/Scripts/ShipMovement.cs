using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipMovement : MonoBehaviour
{
    public GameObject GameManagerGO;

    public GameObject PlayerBulletGO;
    public GameObject BulletPosition01;
    public GameObject BulletPosition02;
    public GameObject ExpliosionGO;
    //Reference to the live ui text
    public TextMeshProUGUI livesUIText;

    const int MaxLives = 3;
    int lives;
    public float firerate = 2f;
    bool isHit = false;
    public void Init()
    {
        lives = MaxLives;

        //Update the live ui text
        livesUIText.text = lives.ToString();

        /*//Reset the player position to the center of the screen 
        transform.position = new Vector2(0, 0);*/

        //Set this game object to active 
        gameObject.SetActive(true);
    }

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //fire bullet when the spacebar is pressed
        if (Input.GetKeyDown("space"))
        {
            if (!isHit)
        {
            isHit = true;
           
                //play the sound effect 
                GetComponent<AudioSource>().Play();
                //instantiate the first bullet
                GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
                bullet01.transform.position = BulletPosition01.transform.position;//set the bullet initial position
                                                                                  //instantiate the second bullet
                GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
                bullet02.transform.position = BulletPosition02.transform.position;//set the bullet initial position
                StartCoroutine(HitCooldown());

            }
        }
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(x, y).normalized;
        Move(direction);
    }
    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0)); // this is the bottom left point (corner)
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // this is the top right point (corner)
        max.x = max.x - 0.225f; //substract the player sprite half width
        min.x = min.x + 0.225f;//add the player sprite half width
        max.y = max.y - 0.285f;
        min.x = min.x + 0.285f;

        //Get the player current position 
        Vector2 pos = transform.position;

        //Calculate player new position
        pos += direction * speed * Time.deltaTime;

        //Make sure the new position is not outside of the screen
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //Update player current position
        transform.position = pos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect collision of the player ship with an enemy or its bullet
        if(collision.tag == "EnemyBulletTag" || collision.tag == "EnemyShipTag")
        {
            PlayerExplosion();
            lives--;
            livesUIText.text = lives.ToString();
            if(lives == 0)
            {
                //Change game mangaer state to game over 
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                //hide the player
                gameObject.SetActive(false);
            }
        }else if(collision.tag == "AsteroidTag")
        {
            PlayerExplosion();
            lives = lives - 2;
            livesUIText.text = lives.ToString();
            if (lives == 0)
            {
                //Change game mangaer state to game over 
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                //hide the player
                gameObject.SetActive(false);
            }
        }else if(collision.tag == "StarCoinTag")
        {
            if (lives < 5)
            {
                lives++;
            }
            if(firerate >= 0.2f)
            {
                firerate = firerate - 0.5f;
            }
            livesUIText.text = lives.ToString();
        }
    }
    void PlayerExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(ExpliosionGO);
        explosion.transform.position = transform.position;
    }
    IEnumerator HitCooldown()
    {
        yield return new WaitForSeconds(firerate);
        isHit = false;
    }
}
