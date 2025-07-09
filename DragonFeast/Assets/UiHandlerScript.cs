using FMOD;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiHandlerScript : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject HP;
    public GameObject hungerSlider;
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

    //fix this
    public void updateHP(int _HP)
    {
        print(_HP);
        for (int i = 0; i < HP.GetComponentsInChildren<RectTransform>().Length; i++)
        {
            if (i <= _HP)
            {
                HP.GetComponentsInChildren<RectTransform>()[i].gameObject.SetActive(true);
            }
            else
            {
                HP.GetComponentsInChildren<RectTransform>()[i].gameObject.SetActive(false);
            }
        }
    }

    public void updateHungerBar(float _hunger)
    {
        hungerSlider.GetComponent<Slider>().value = _hunger;
    }
}
