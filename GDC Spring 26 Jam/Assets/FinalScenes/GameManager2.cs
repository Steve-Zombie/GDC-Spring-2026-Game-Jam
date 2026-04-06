using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{

    public Button restartButton;
    public Button quitGame;
    
    void Start()
    {
        restartButton.onClick.AddListener(restartGame);
        quitGame.onClick.AddListener(quitGameFunction);

    }

    public void restartGame()
    {
        SceneManager.LoadScene("MainLevel");

    }

    public void quitGameFunction()
    {
        Application.Quit();
    }
}
