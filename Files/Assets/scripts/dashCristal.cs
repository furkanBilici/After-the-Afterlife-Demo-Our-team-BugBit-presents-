using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashCristal : MonoBehaviour
{
    public MovementPlayer character;
    bool control;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (character.GetComponent<MovementPlayer>().IsGrounded)
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
            control = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character" && control)
        {
            this.GetComponent<SpriteRenderer>().color = Color.black; 
            character.GetComponent<MovementPlayer>().ekDash = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character" )
        {
            
            control = false;
            
        }
    }
}
