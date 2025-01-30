using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asansor : MonoBehaviour
{
    public GameObject yesil;
    public GameObject asansor;
    bool canmove = false;
    public MovementPlayer player;
    public bool stop;
    // Start is called before the first frame update
    void Start()
    {
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!stop)
        {
            Move();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            yesil.SetActive(true);
            canmove = true;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            player.CanMove = false;
        }
        if(collision.gameObject.tag == "end")
        {
            stop = true;
        }
    }
    private void Move()
    {
        if (canmove) 
        {
            asansor.transform.position=new Vector3(asansor.transform.position.x,asansor.transform.position.y+0.025f,asansor.transform.position.z);    
        }
    }
}
