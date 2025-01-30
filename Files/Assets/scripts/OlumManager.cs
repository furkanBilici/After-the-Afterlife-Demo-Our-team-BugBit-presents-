using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OlumManager : MonoBehaviour
{
    // Start is called before the first frame update
    public MovementPlayer player;
    public GameObject Ui;
    public GameObject Character;
    public Vector3 checkpoint;
    public bool CanCheck;
    public GameObject check;

    void Start()
    {
        CanCheck = false;
    }

    // Update is called once per frame
    void Update()
    {
        Dead();
    }
    void Dead()
    {
        if (player.GetComponent<MovementPlayer>().IsDead)
        {
            Ui.SetActive(true);
            Ui.GetComponent<CanvasGroup>().DOFade(1,2f);
            Character.SetActive(false);
            if (CanCheck)
            {
                check.SetActive(true);
            }
        }
    }
    public void TekrarDene()
    {
        SceneManager.LoadScene(1);
    }
    public void Checkpoint()
    {

            player.GetComponent<MovementPlayer>().IsDead = false;
            Ui.SetActive(false);
            Ui.GetComponent<CanvasGroup>().DOFade(0, 2f);
            Character.transform.position = checkpoint;
            Character.SetActive(true);
        
    }
}
