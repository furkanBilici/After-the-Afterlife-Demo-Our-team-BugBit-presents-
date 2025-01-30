using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSC : MonoBehaviour
{
    [SerializeField]
    public GameObject target;
    public Vector3 cameraOffset;
    public Vector3 targetPosition;
    public Vector3 velocity = Vector3.zero;
    public float worldBorderLeft, worldBorderRight, worldBorderUp;
    

    public float smoothTime;

    private void Start()
    {
        transform.position = new Vector3(-18, 7, -1);
    }
    private void Update()
    {
        Follow();
    }
    void Follow()
    {
        if (target.transform.position.x >= worldBorderLeft && target.transform.position.x <= worldBorderRight)
        {
            targetPosition = target.transform.position + cameraOffset;
            transform.position = new Vector3(targetPosition.x,transform.position.y, transform.position.z);

        }
        if ((target.transform.position.y <= worldBorderUp))
        {
            targetPosition = target.transform.position + cameraOffset;
            transform.position = new Vector3(transform.position.x, targetPosition.y,transform.position.z);

        }

    }
    
}
