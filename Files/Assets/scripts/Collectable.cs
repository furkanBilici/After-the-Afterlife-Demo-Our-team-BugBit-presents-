using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float speed = 3.0f; 
    public float amplitude = 0.4f; 
    private Vector3 initialPosition;
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }
    public void movement()
    {
        float newY = initialPosition.y + Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            this.gameObject.SetActive(false);
        }
    }
}
