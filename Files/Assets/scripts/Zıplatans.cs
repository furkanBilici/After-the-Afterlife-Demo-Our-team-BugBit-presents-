using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zıplatans : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rb;
    public float power;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            rb.AddForce(Vector3.up * power, ForceMode2D.Impulse);
            anim.SetBool("ziplatir", true);
            Invoke("Ziplat", 0.2f);
        }
        
    }
    void Ziplat()
    {
        anim.SetBool("ziplatir", false);
    }
}
