using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float speed; 
    public bool isMoving; //the flag to make the planet scroll down the screen 

    Vector2 min;
    Vector2 max;

    void Awake()
    {
        isMoving = false;
        min =  Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max =  Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //add the planet sprite half height to max y 
        max.y  = max.y + GetComponent<SpriteRenderer>().sprite.bounds.extents.y;

        //substract the planet sprite half height to min y 
        min.y = min.y - GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            return;
        }
        //Get the current postion
        Vector2 pos = transform.position;

        pos = new Vector2(pos.x, pos.y + speed * Time.deltaTime);

        transform.position = pos;   
        
        if(transform.position.y < min.y)
        {
            isMoving = false;
        }
    }
    public void ResetPosition()
    {
        //reset the position of planet to random x and max y 
        transform.position = new Vector2(Random.Range(min.x, max.x), max.y);
    }
}
