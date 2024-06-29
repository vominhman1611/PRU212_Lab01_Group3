using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public GameObject[] Planet;

    //Queue to hold the planet 
    Queue<GameObject> availablePlanets = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        availablePlanets.Enqueue(Planet [0]);
        availablePlanets.Enqueue(Planet [1]);
        availablePlanets.Enqueue(Planet [2]);

        //call MovePlanetDown
        InvokeRepeating("MovePlanetDown", 0, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function to dequeue planet to make them scroll down the screen 
    void MovePlanetDown()
    {
        EnqueuePlanet();
        //if quque empty return
        if (availablePlanets.Count == 0) return;

        //Get a planet from queue 
        GameObject planet = availablePlanets.Dequeue();

        //Set the flag isMoving to true
        planet.GetComponent<Planet>().isMoving = true;
    }
    
    //Function to make planet below the screen to enqueue once again \
    void EnqueuePlanet()
    {
        foreach(GameObject planet in  availablePlanets)
        {
            if((planet.transform.position.y < 0) && (!planet.GetComponent<Planet>().isMoving))
            {
                //reset the planet positon 
                planet.GetComponent<Planet>().ResetPosition();

                //Enqueue the planet
                availablePlanets.Enqueue(planet);
            }
        }
    }
}
