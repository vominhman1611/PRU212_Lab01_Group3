using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    float speed; //the bullet speed
    Vector2 _direction;//the bullet direction
    bool isReady;//to know when the bullet direction is set 
    // Start is called before the first frame update
    void Awake()
    {
        speed = 5f;
        isReady = false;
    }
    void Start()
    {
        
    }
    public void SetDirection(Vector2 direction)
    {
        //set the direction normalized, to get an unit vector 
        _direction = direction.normalized;

        isReady = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            //get the bullet current's position
            Vector2 position = transform.position;

            //Compute the bullet's new position
            position += _direction * speed * Time.deltaTime;

            //update the bullet's position
            transform.position = position;

            //remove bullet if go outside
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));//bottom-left screen 

            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 2));//top-right screen

            if (transform.position.x < min.x 
                || transform.position.x > max.x 
                || transform.position.y < min.y 
                || transform.position.y > max.y)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerShipTag")
        {
            Destroy(gameObject);
        }
    }
}
