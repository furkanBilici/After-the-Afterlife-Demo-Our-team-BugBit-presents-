using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public MovementPlayer character;
    public Collider2D portal;
    public int salterSayisi;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        KapiAc();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
    void KapiAc()
    {
        if (character.key == salterSayisi)
        {
            character.key = 0;
            portal.isTrigger = true;
            animator.SetBool("Acik", true);
        }
    }
}
