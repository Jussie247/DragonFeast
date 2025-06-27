using UnityEngine;

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
}
