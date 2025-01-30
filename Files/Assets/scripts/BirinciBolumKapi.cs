using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirinciBolumKapi : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canPass;
    void Start()
    {
        canPass = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            canPass=true;    
        }
    }
}
