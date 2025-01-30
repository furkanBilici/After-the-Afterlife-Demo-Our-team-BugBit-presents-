using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEditor;

public class BirinciBolumManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Mahzen;
    public GameObject Karanlik;
    bool Shaker=false;
    public GameObject kapi;
    public GameObject menu;
    public AudioSource deprem;
    public AudioClip KapiKirilma;

    void Start()
    {
        
        originalPosition = cameraTransform.localPosition;
        Invoke("Shake",8f);
        Invoke("KaraLord", 8.3f);
        Invoke("KapiKir", 9f);

    }
    private void Update()
    {
        OpenCloseMenu();
        KapýKontrol();
        if (Shaker)
        {
            if (shakeDuration > 0)
            {
                cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
                shakeDuration -= Time.deltaTime;
            }
            else
            {
                cameraTransform.localPosition = originalPosition;
            }
        }
    }

    void KapiKir()
    {
        deprem.PlayOneShot(KapiKirilma);
        Mahzen.SetActive(true);
    }
    void KaraLord()
    {
        Karanlik.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
        Karanlik.GetComponent<CanvasGroup>().DOFade(0, 0.8f).SetDelay(0.8f);

    }
    public Transform cameraTransform;
    public float shakeDuration = 0.2f;
    public float shakeAmount = 0.1f;

    private Vector3 originalPosition;


    private void Shake()
    {
        deprem.Play();
       Shaker = true;
    }

    public void TriggerShake()
    {
        shakeDuration = 0.2f;
    }
    void KapýKontrol()
    {
        if (kapi.GetComponent<BirinciBolumKapi>().canPass)
        {
            Invoke("Shake", 0.2f);
            Invoke("KaraLord", 0.5f);
            Invoke("KapiKir", 1f);
            Invoke("SahneDegistir", 1f);
           
        }
    }
    void SahneDegistir()
    {
        SceneManager.LoadScene(2);
    }
    void OpenCloseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menu.activeSelf)
        {
            Time.timeScale = 0;
            menu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menu.activeSelf)
        {
            Time.timeScale = 1;
            menu.SetActive(false);
        }

    }
    public void Continue()
    {
        Time.timeScale = 1;
        menu.SetActive(false);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}
