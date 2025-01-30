using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sıfırlama : MonoBehaviour
{
    public Vector2 StartPosition;
    // Start is called before the first frame update
    
    public void yeniden()
    {
        transform.position = StartPosition;
    }
}
