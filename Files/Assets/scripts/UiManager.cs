using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject quitButton;
    // Start is called before the first frame update
    void Start()
    {
        quitButton.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
