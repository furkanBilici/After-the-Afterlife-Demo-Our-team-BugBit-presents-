using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Who;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void AnswerQuestion()
    {
        if (!Who.activeSelf)
        {
            Who.SetActive(true);
        }
        
    }
    public void CloseWho()
    {
        if (Who.activeSelf)
        {
            Who.SetActive(false);
        }
    }
}
