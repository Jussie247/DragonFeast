using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiHandlerScript : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject HP;
    [SerializeField] GameObject hungerSlider;
    [SerializeField] GameObject sensitivitySlider;

    public static float sensitivity = 0.5f;
    public static bool paused;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pauseScreen.SetActive(false);
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pause()
    {
        paused = true;
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void resume()
    {
        paused=false;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
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

    public void updateSensitivity()
    {
        sensitivity = sensitivitySlider.GetComponent<Slider>().value;
    }
}
