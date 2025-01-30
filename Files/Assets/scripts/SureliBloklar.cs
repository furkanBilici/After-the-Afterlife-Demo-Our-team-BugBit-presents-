using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SureliBloklar : MonoBehaviour
{
    public GameObject blok1, blok2;
    float sure;
    public float frekans;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SureliBlok();
      
    }
    void SureliBlok()
    {
        sure=Mathf.Sin(frekans*Time.time);
        if (sure > 0)
        {
            blok2.GetComponent<SpriteRenderer>().color = Color.grey;
            blok1.GetComponent<SpriteRenderer>().color = Color.white;
            blok2.GetComponent<Collider2D>().isTrigger = true;
            blok1.GetComponent<Collider2D>().isTrigger = false;
        }
        if (sure < 0)
        {
            blok1.GetComponent<SpriteRenderer>().color = Color.grey;
            blok2.GetComponent<SpriteRenderer>().color = Color.white;
            blok1.GetComponent<Collider2D>().isTrigger = true;
            blok2.GetComponent<Collider2D>().isTrigger = false;
        }
    }
}
