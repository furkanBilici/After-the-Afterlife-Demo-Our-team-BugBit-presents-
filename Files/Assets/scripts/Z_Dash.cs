using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Z_Dash : MonoBehaviour
{
    public Vector3 Yon;
    public Rigidbody2D rb;
    public MovementPlayer player;
    public Animator animator;
    void Start()
    {
    }


    void Update()
    {
        
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "character")
        {
            rb.AddForce(Yon * player.speed *2.2f, ForceMode2D.Impulse);
            player.sayac++;
            animator.SetBool("IsActive",true);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "character")
        {
            animator.SetBool("IsActive", false);
        }
    }

}
