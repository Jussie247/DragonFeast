using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandeler : MonoBehaviour
{
    public GameObject mainUI;
    public GameObject shopUI;
    public GameObject levelUI;
    public GameObject skillsUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shopUI.SetActive(false);
        levelUI.SetActive(false);
        skillsUI.SetActive(false);

        mainUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void back()
    {
        shopUI.SetActive(false);
        levelUI.SetActive(false);
        skillsUI.SetActive(false);

        mainUI.SetActive(true);
    }

    public void shop()
    {
        mainUI.SetActive(false);
        shopUI.SetActive(true);
    }
    public void level()
    {
        mainUI.SetActive(false);
        levelUI.SetActive(true);
    }
    public void skills()
    {
        mainUI.SetActive(false);
        skillsUI.SetActive(true);
    }
}
