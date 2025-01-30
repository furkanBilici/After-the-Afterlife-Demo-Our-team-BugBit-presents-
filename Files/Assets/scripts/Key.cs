using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Animator animator;
    public MovementPlayer player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "character")
        {
            
            if (!animator.GetBool("Salter"))
            {
                player.key++;
            }
            animator.SetBool("Salter", true);
        }
    }
}
