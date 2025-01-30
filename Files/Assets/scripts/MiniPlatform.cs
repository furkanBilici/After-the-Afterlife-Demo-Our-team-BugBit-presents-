using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPlatform : MonoBehaviour
{
    public GameObject platform;
    public EdgeCollider2D kafa;
    public BoxCollider2D kutu;
    void Start()
    {
        platform.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        STusunaBasýldýmý();

    }
    void STusunaBasýldýmý()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            kutu.isTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character" )
        {
            kutu.isTrigger = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character" && kafa)
        {
           kutu.isTrigger = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "character" )
        {

            kutu.isTrigger = true;
        }
    }

}
