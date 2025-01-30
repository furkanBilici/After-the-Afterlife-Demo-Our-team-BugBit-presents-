using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontBackChecker : MonoBehaviour
{
    public bool touching= false;
    public bool touchingSideGround=false;   
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            touching= true;
        }
        if(collision.gameObject.tag == "sideGround" || collision.gameObject.tag == "Ground")
        {
            touchingSideGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            touching= false;
        }
        if (collision.gameObject.tag == "sideGround" || collision.gameObject.tag == "Ground")
        {
            touchingSideGround = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            touching = true;
        }
        if(collision.gameObject.tag == "sideGround" || collision.gameObject.tag == "Ground")
        {
            touchingSideGround = true;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "sideGround" || collision.gameObject.tag == "Ground")
        {
           
            touchingSideGround = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "sideGround" || collision.gameObject.tag == "Ground")
        {
           
            touchingSideGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "sideGround" || collision.gameObject.tag == "Ground")
        {
            touchingSideGround = false;
        }
    }
}
