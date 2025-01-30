using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEditor;
using System;

public class MenuManager : MonoBehaviour
{
    public GameObject yazi, start, exit, load;
    public SaveManager savemanager;
    void Start()
    {
        StartAnimation();
        Debug.Log(savemanager.a);
    }

    public void NewGame()
    {        
        SceneManager.LoadScene(1);
        savemanager.CheckpointSave(new Vector3 (0, 0, 0));
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    void StartAnimation()
    {
        yazi.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
        start.GetComponent<CanvasGroup>().DOFade(1, 0.2f).SetDelay(0.2f);
        load.GetComponent<CanvasGroup>().DOFade(1, 0.2f).SetDelay(0.4f);
        exit.GetComponent<CanvasGroup>().DOFade(1, 0.2f).SetDelay(0.6f);
        
    }
}
