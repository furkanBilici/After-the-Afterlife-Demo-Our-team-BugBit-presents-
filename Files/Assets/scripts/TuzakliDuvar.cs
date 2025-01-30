using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class TuzakliDuvar : MonoBehaviour
{
    public GameObject spike;

    void Start()
    { }

    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            Invoke("DikenBatir", 0.5f);
        }
    }
    void DikenBatir()
    {
        spike.SetActive(true);
    }
 

}
