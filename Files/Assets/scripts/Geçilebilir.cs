using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geçilebilir : MonoBehaviour
{
    public Collider2D blok;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        STusunaBasıldımı();
    }   
    void STusunaBasıldımı()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            blok.GetComponent<Collider2D>().isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            blok.GetComponent<Collider2D>().isTrigger = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            blok.GetComponent<Collider2D>().isTrigger = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            blok.GetComponent<Collider2D>().isTrigger = true;
        }
    }

}
