using UnityEngine;
using UnityEngine.SceneManagement;

public class UiHandlerScript : MonoBehaviour
{
    public GameObject pauseScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pause()
    {
        pauseScreen.SetActive(true);
    }

    public void resume()
    {
        pauseScreen.SetActive(false);
    }

    public void quit()
    {
        Application.Quit();
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
