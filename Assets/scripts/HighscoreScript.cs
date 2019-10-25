using UnityEngine;
using UnityEngine.UI;

public class HighscoreScript : MonoBehaviour
{
    private Text highscoreText;
    private float highscore;
    private bool stopped;

    void Awake()
    {
        GameObject highscoreText1 = GameObject.Find("HighscoreNumber");
        highscoreText = highscoreText1.GetComponent<Text>();
    }

    void Start()
    {
        highscore = 0f;
        highscoreText.text = "0";

        stopped = false;
    }

    void Update()
    {
        if(!stopped)
        {
            highscore += Time.deltaTime;
            highscoreText.text = Mathf.Round(highscore).ToString();
        }
    }

    public float getHighscore
    {
        get
        {
            return highscore;
        }
    }

    public void addToHighscore(float number)
    {
        highscore += number;
    }

    public void stopHighscore()
    {
        stopped = true;
    }
}