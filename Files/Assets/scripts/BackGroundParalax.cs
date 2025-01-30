using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundParalax : MonoBehaviour
{
    float lenght, height;
    Transform startpos;
    public float paralax;
    public float paralaxx;
    public GameObject cam;
    float starty, startx;

    // Start is called before the first frame update
    void Start()
    {
        // startpos atanýyor
        startpos = transform;
        startx = startpos.position.x;
        starty = startpos.position.y;

        // SpriteRenderer boyutlarý alýnýr
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
        height = GetComponent<SpriteRenderer>().bounds.size.y;

        // Kamera kontrolü
        if (cam == null)
        {
            Debug.LogError("Camera GameObject is not assigned!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (cam == null) return;

        float parallaxX = cam.transform.position.x * paralaxx;
        float parallaxY = cam.transform.position.y * paralax;

        float tempx = cam.transform.position.x * (1 - paralaxx);
        float tempy = cam.transform.position.y * (1 - paralax);

        transform.position = new Vector3(startx + parallaxX, starty + parallaxY, startpos.position.z);

        if (tempx > startx + lenght) startx += lenght;
        else if (tempx < startx - lenght) startx -= lenght;

        if (tempy > starty + height) starty += height;
        else if (tempy < starty - height) starty -= height;
    }
}
