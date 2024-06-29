using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject starGO;
    public int MaxStar;

    //Array of Colors
    Color[] starColor ={
        new Color(0.5f, 0.5f, 1f), //blue
        new Color(0, 1f, 1f), //green
        new Color (1f, 1f, 0), //yellow
        new Color (1f, 0, 0), //red
    };
    // Start is called before the first frame update
    void Start()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //loop create the stars 
        for(int i =0; i < MaxStar; ++i)
        {
            GameObject star = (GameObject)Instantiate(starGO);

            //Set the star color 
            star.GetComponent<SpriteRenderer>().color = starColor[i % starColor.Length];  

            //set the position of the star  
            star.transform.position = new Vector2 (Random.Range(min.x, max.x), Random.Range(min.y, max.y));

            //set a random speed for star 
            star.GetComponent<Star>().speed = -(1f * Random.value + 0.5f);

            //make the star a child of the StarGeneratorGO
            star.transform.parent = transform;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
