using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HellDemoManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject menu;
    public GameObject Character;
    public GameObject black;
    public GameObject text;
    public List<Sýfýrlama> reloadNeed;
    public GameObject BackgroundMusic;
    public GameObject ONCanvas;
    public GameObject OFFCanvas;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(FinishDemo());
        OpenCloseMenu();
        GameOver();
        
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
    void GameOver()
    {
        if (Character.GetComponent<MovementPlayer>().IsDead)
        {
            foreach (var _object in reloadNeed)
            {
                _object.yeniden();
                _object.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            Character.GetComponent<MovementPlayer>().IsDead = false;
            Time.timeScale = 1;
            Character.transform.position = new Vector3(-18.9f,0,0);
        }
    }
    public void Music_ON()
    {
        BackgroundMusic.SetActive(false);
        ONCanvas.SetActive(false);
        OFFCanvas.SetActive(true);
    }
    public void Music_OFF()
    {
        BackgroundMusic.SetActive(true);
        ONCanvas.SetActive(true);
        OFFCanvas.SetActive(false);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public IEnumerator FinishDemo()
    {
        if (Character.GetComponent<MovementPlayer>().end)
        {
            black.SetActive(true);
            Color originalColor = black.GetComponent<Image>().color; // Mevcut rengi al
            float elapsedTime = 0f;

            // Black nesnesinin opaklýðýný arttýr
            while (elapsedTime < 2f)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsedTime / 2f); // 0'dan 1'e interpolasyon
                black.GetComponent<Image>().color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
                yield return null; // Bir sonraki frame'e kadar bekle
            }

            // Text nesnesini göster ve opaklýðýný arttýr
            text.SetActive(true);
            Color originalTextColor = text.GetComponent<TextMeshProUGUI>().color; // TextMeshPro rengi al
            elapsedTime = 0f;

            while (elapsedTime < 2f)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Clamp01(elapsedTime / 2f); // 0'dan 1'e interpolasyon
                text.GetComponent<TextMeshProUGUI>().color = new Color(originalTextColor.r, originalTextColor.g, originalTextColor.b, alpha);
                yield return null; // Bir sonraki frame'e kadar bekle
            }

            // Bekleme süresi (isteðe baðlý)
            elapsedTime = 0f;
            while (elapsedTime < 2f)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            MainMenu(); // Ana menüye dön
        }
    }
}
