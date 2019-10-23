using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    private GenerateScript[] generateScripts;
    private HighscoreScript highscoreScript;
    private GameOverOverlayScript gameOverOverlayScript;

    void Awake()
    {
        highscoreScript = FindObjectOfType<HighscoreScript>();
        generateScripts = FindObjectsOfType<GenerateScript>();
        gameOverOverlayScript = FindObjectOfType<GameOverOverlayScript>();
    }

    public void EndGame()
    {
        StopHighscore();
        StopGenerating();
        gameOverOverlayScript.ShowPanel();
    }

    public void StopHighscore()
    {
        if(highscoreScript != null)
        {
            highscoreScript.stopHighscore();
        }
    }

    public void StopGenerating()
    {
        foreach( GenerateScript generateScript in generateScripts )
        {
            generateScript.setFreeze(true);
        }
    }

    public void ExitToGameMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Stage1");
    }
}