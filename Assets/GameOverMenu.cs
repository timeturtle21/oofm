using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static bool gameIsOver = false;
    public GameObject GameOverMenuUI;

    public Text waves, score, time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameIsOver)
        {
            GameOverMenuUI.SetActive(true);
            Time.timeScale = 0f;
            waves.text = "Waves: ";//+ CanvasController.currentLevel.ToString();
            score.text = "Score: ";// + CanvasController.currentScore.ToString();
            time.text = "Time: ";//+ CanvasController.timerText.text;
        }
    }

    public void loadMenu()
    {
        gameIsOver = false;
        GameOverMenuUI.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUITING GAME");
        Application.Quit();
    }
}