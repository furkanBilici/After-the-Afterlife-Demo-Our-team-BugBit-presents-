using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public GameObject myObject;
    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            animator.SetBool("destroy", true);
            Invoke("Destroy", 3f);
            

        }
    }
    public void Destroy()
    {
        myObject.SetActive(false);
        Invoke("DestroyNot", 3f);
    }
    public void DestroyNot()
    {
        animator.SetBool("destroy", false);
        myObject.SetActive(true);
    }

}
