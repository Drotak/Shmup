using UnityEngine;
using UnityEngine.UI;

public class GameOverOverlayScript : MonoBehaviour
{
    private Button[] buttons;
    private Transform highscoreObject;
    void Awake()
    {
        buttons = gameObject.GetComponentsInChildren<Button>();
        highscoreObject = this.transform.Find("Highscore");
        HidePanel();
    }

    public void HidePanel()
    {
        highscoreObject.gameObject.SetActive(false);

        foreach(Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void ShowPanel()
    {
        Text highscoreText = highscoreObject.GetComponent<Text>();

        HighscoreScript highscoreScript = FindObjectOfType<HighscoreScript>();
        highscoreText.text = "Highscore: " + Mathf.Round(highscoreScript.getHighscore).ToString();

        foreach(Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }
}