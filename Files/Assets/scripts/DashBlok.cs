using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashBlok : MonoBehaviour
{
    public Animator character;
    public Collider2D Wall;
    public Rigidbody2D chracterRb;
    
    void Update()
    {
        Pass();
    }
    public void Pass()
    {
        if (character.GetBool("IsDash"))
        {
            Wall.isTrigger = true;
        }        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            Wall.isTrigger = false;
            character.SetBool("IsDash", false);
            chracterRb.gravityScale = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            chracterRb.gravityScale = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "character" && character.GetBool("IsDash"))
        {
            Wall.isTrigger = true;
        }
    }
}
