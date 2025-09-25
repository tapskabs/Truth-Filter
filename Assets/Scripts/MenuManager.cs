using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
   
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

  
    public void HowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

   
    public void StartFromHowToPlay()
    {
        SceneManager.LoadScene("MainScene");
    }

   
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

   
    public void QuitGame()
    {
        Application.Quit();
    }
}
