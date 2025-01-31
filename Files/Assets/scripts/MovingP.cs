using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class MovingP : MonoBehaviour
{

    float targetX;
    float targetY;
    public Transform targetPosition;
    bool canMove;
    public Transform Character;
    bool IsMove=false;
    public float speed;
    public int stage = 1;
    bool isCharacterTouching;
    public bool finished;
    public Vector3 StartPosition;
    public float time=0;
    
    void Start()
    {
        time = 0;
        if (stage == 1)
        {
            finished = false;
            canMove = false;
            targetX = targetPosition.position.x;
            targetY = targetPosition.position.y;
        }
    }
    private void Update()
    {
        time+=Time.deltaTime;
        if (targetPosition.position == transform.position) { finished = true; }
    }
    void FixedUpdate()
    {
        if (stage == 1)
        {
            MoveX1();
            //MoveY();
        }
        if (stage == 2)
        {
            MoveX2();
        }
    }
    void MoveX1()
    {
            if (canMove && (targetX > transform.position.x) && !finished)
            {
                IsMove = true;
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
                if (isCharacterTouching && IsMove)
                {
                    Character.position = new Vector3(Character.position.x + speed, Character.position.y, Character.position.z);
                }
            }
            else if ( canMove && (targetX < transform.position.x) && !finished)
            {
                IsMove = true;
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
                if (isCharacterTouching && IsMove)
                {
                    Character.position = new Vector3(Character.position.x - speed, Character.position.y, Character.position.z);
                }
            }
            else
            {
                IsMove = false;
            }
    }
    /*void MoveY()
    {
            if (canMove && (targetY > transform.position.y))
            {
                IsMove = true;
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
                if (isCharacterTouching && IsMove)
                {
                    Character.position = new Vector3(Character.position.x, Character.position.y + speed, Character.position.z);
                }
            }
            else if (canMove && (targetY < transform.position.y))
            {
                IsMove = true;
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
                if (isCharacterTouching && IsMove)
                {
                    Character.position = new Vector3(Character.position.x, Character.position.y - speed, Character.position.z);
                }
            }
            else
            {
                IsMove = false;
            }
    }*/
    void MoveX2()
    {
        float x = Mathf.Cos(time) * speed;
        
        Vector3 yeniPozisyon = new Vector3(this.transform.position.x + x, this.transform.position.y, this.transform.position.z);
        transform.position = yeniPozisyon;
        if (isCharacterTouching)
        {
            Character.position = new Vector3(Character.position.x + x, Character.position.y, Character.position.z);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            canMove = true;
            isCharacterTouching = true;
        }  
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            isCharacterTouching = false;
        }
        
    }

}
