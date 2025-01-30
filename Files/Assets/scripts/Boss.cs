using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boss : MonoBehaviour
{
    public GameObject target;

    public Vector3 targetPosition;
    public Vector3 velocity = Vector3.zero;


    public float smoothTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
    void Follow()
    {
        
        targetPosition = target.transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        if (targetPosition.x-transform.position.x > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        if (targetPosition.x - transform.position.x < 0)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
    }
}
