using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScore : MonoBehaviour
{
    TextMeshProUGUI scoreTextUI;
    int score;
    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            this.score = value;
            UpdateScoreTextUI();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get the TextUI component of this game object
        scoreTextUI = GetComponent<TextMeshProUGUI>();
    }
    //Function to update the score text UI 
    void UpdateScoreTextUI()
    {
        string scoreStr = string.Format("{0:000000}", score);
        scoreTextUI.text = scoreStr;
    }
}
