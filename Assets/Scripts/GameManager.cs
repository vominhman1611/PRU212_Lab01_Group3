using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public GameObject playButton;
    public GameObject quitButton;
    public GameObject insButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject asteroidSpawner;
    public GameObject starcoinSpawner;
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject TimeCounterGO;
    public GameObject GameTitleGO;
    public enum GameManagerState
    {
        GamePlay,
        GameOver,
        Opening,
    }
    GameManagerState state;
    // Start is called before the first frame update
    void Start()
    {
        state = GameManagerState.Opening;
    }

    // Update is called once per frame
    void UpdateGameManagerState()
    {
        switch(state){
            case GameManagerState.GamePlay:
                //hide the play button
                playButton.SetActive(false);
                //hide the quit button
                quitButton.SetActive(false);
                //hide the instruction button
                insButton.SetActive(false);
                //hide game title
                GameTitleGO.SetActive(false);

                //Reset the score 
                scoreUITextGO.GetComponent<GameScore>().Score = 0;

                //set the player visible (active) and init the player lives
                playerShip.GetComponent<ShipMovement>().Init();

                //Start enemy spawner 
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                //Start enemy spawner 
                asteroidSpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
                //Start star coin spawner 
                starcoinSpawner.GetComponent<StarCoinSpawner>().ScheduleEnemySpawner(); 
                //start the time counter 
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

                break; 
            case GameManagerState.GameOver:
                //Stop the time counter
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();
                //Stop enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().UnScheduleEnemySpawner();
                //Stop enemy spawner
                asteroidSpawner.GetComponent<EnemySpawner>().UnScheduleEnemySpawner();
                //Stop enemy spawner
                starcoinSpawner.GetComponent<StarCoinSpawner>().UnScheduleEnemySpawner();
                //Display game over 
                GameOverGO.SetActive(true);

                //Change game manager state 
                Invoke("ChangeToOpeningState", 4f);


                break;
            case GameManagerState.Opening:

                //Stop enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().UnScheduleEnemySpawner();
                //Stop asteroid spawner
                asteroidSpawner.GetComponent<EnemySpawner>().UnScheduleEnemySpawner();
                //Stop star coin spawner
                starcoinSpawner.GetComponent<StarCoinSpawner>().UnScheduleEnemySpawner();
                //Hide game over 
                GameOverGO.SetActive(false);
                //set play button visible (active)
                playButton.SetActive(true);
                //set exit button
                quitButton.SetActive(true);
                //hide the instruction button
                insButton.SetActive(true);
                //Set game title
                GameTitleGO.SetActive(true);



                break;
        }
    }
    //Function to set the game manager state 
    public void SetGameManagerState(GameManagerState states)
    {
        state = states;
        UpdateGameManagerState();
    }
    public void StartGameplay()
    {
        state = GameManagerState.GamePlay;
        UpdateGameManagerState();
    }
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
    public void ExitGame()
    {
       // SceneManager.LoadSceneAsync(0);
        UnityEngine.Debug.Log("Quit success");
    }
}
