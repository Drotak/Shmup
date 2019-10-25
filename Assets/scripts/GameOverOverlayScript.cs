using UnityEngine;
using UnityEngine.UI;

public class GameOverOverlayScript : MonoBehaviour
{
    private Button[] buttons;
    private Transform highscoreObject;
    private Transform imageObject;
    void Awake()
    {
        buttons = gameObject.GetComponentsInChildren<Button>();
        highscoreObject = this.transform.Find("Highscore");
        imageObject = transform.Find("Background");
        HidePanel();
    }

    public void HidePanel()
    {
        highscoreObject.gameObject.SetActive(false);
        imageObject.gameObject.SetActive(false);

        foreach(Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void ShowPanel()
    {
        Text highscoreText = highscoreObject.gameObject.GetComponent<Text>();

        HighscoreScript highscoreScript = FindObjectOfType<HighscoreScript>();
        highscoreText.text = "Score: " + Mathf.Round(highscoreScript.getHighscore).ToString();

        highscoreObject.gameObject.SetActive(true);
        imageObject.gameObject.SetActive(true);

        foreach(Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }
}