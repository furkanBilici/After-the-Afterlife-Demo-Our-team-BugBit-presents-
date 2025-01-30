using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OlumculNesne : MonoBehaviour
{

    public MovementPlayer player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            player.GetComponent<MovementPlayer>().IsDead = true;
        }
    }   
}