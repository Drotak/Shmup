using UnityEngine;
using UnityEngine.UI;

public class GameOverOverlayScript : MonoBehaviour
{
    private Button[] buttons;
    void Awake()
    {
        buttons = gameObject.GetComponentsInChildren<Button>();
        HidePanel();
    }

    public void HidePanel()
    {
        foreach(Button button in buttons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void ShowPanel()
    {
        Transform highscoreText1 = this.transform.Find("Highscore");
        Text highscoreText = highscoreText1.GetComponent<Text>();

        HighscoreScript highscoreScript = FindObjectOfType<HighscoreScript>();
        highscoreText.text = "Highscore: " + Mathf.Round(highscoreScript.getHighscore).ToString();

        foreach(Button button in buttons)
        {
            button.gameObject.SetActive(true);
        }
    }
}