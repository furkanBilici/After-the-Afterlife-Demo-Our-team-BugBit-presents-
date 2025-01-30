using DG.Tweening;
using DG.Tweening.Core.Easing;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;

using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class MovementPlayer : MonoBehaviour
{
    public FrontBackChecker back;
    public FrontBackChecker front;
    public int YonDuvar;//karakter saða bakýyorsa 1 sola bakýyorsa -1 döndürür duvardayken geri kalan yerlerde 0 döndürür.
    public Rigidbody2D rb;
    Vector3 velocity;
    public float speed = 6f;
    public float JumpForce = 3f;
    public Animator animator;
    public int _ekDash;
    public Camera cam;

   
    public int ekDash
    {
        get
        {
            return _ekDash;
        }
        set
        {

            if (value > 1)
            {
                _ekDash = 1;
            }
            else
            {
                _ekDash = value;
            }
        }
    }
    int dashBitiren = 0;
    public int _sayac;
    public int sayac
    {
        get
        {
            return _sayac;
        }
        set
        {

            if (value > 1)
            {
                _sayac = 1;
            }
            else
            {
                _sayac = value;
            }
        }
    }
    [HideInInspector]
    public bool IsGrounded;
    public bool CanMove = true;

    //[HideInInspector]
    public bool IsDead;
    [HideInInspector]
    public int key = 0;
    public float WallWaitTime = 5f;

    public float DashGroundWaitTime = 0.5f;
   
    public float DashTime = 1f;
    public SaveManager savemanager;
    

    public bool _isWall;
    bool canJump = true;
    bool grabbing;

    public float colliderYukseklik;


    public BoxCollider2D col;
    public bool end;

    public GameObject MovingP;

    [SerializeField]
    public GameObject player;
    int _jumpcount;
    public int jumpCount
    {
        get
        {
            return _jumpcount;
        }
        set
        {

            if (value > 1)
            {
                _jumpcount = 1;
            }
            else
            {
                _jumpcount = value;
            }
        }
    }

    void Start()
    {
        grabbing = false;
        StartCheckpoint();
        CanMove = true;
        sayac = 1;
        Rigidbody rb = GetComponent<Rigidbody>();
        jumpCount = 1;
        IsDead = false;
        colliderYukseklik = col.offset.y;
        savemanager.a++;
        Debug.Log(savemanager.a);
    }

    void Update()
    {
        
        transform.rotation = quaternion.Euler(0, 0, 0);
        Move();
        CharacterJump();
        fall();
        EkDashControl();
        DashAnim();
    }
    private void FixedUpdate()
    {
        FallWall();
        //DuvardanDus();
        DashAt();
    }

    private void Move()
    {
        CharacterRotation();
        if (CanMove)
        {
            LeftMove();
            RightMove();
        }
        Climbb();
        CharacterJump();
       

        CharacterRotation();
    }
    void LeftMove()
    {
        if (!front.touchingSideGround && !front.touching)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 && !animator.GetBool("IsDash") && IsGrounded)
            {
                animator.SetBool("IsRun", true);
            }

            if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !animator.GetBool("IsWall"))
            {
                velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
                transform.position += velocity * speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && animator.GetBool("IsWall") && IsGrounded)
            {
                velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
                transform.position += velocity * speed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && animator.GetBool("IsWall") && Input.GetKeyDown(KeyCode.N))
            {

            }
        }
        else { animator.SetBool("IsRun", false); }
        if ((Mathf.Abs(Input.GetAxis("Horizontal")) == 0 || !IsGrounded))
        {
            animator.SetBool("IsRun", false);
        }
    }
    void RightMove()
    {
        if (!front.touchingSideGround && !front.touching)
        {
            if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 && !animator.GetBool("IsDash") && IsGrounded)
            {
                animator.SetBool("IsRun", true);
            }

            if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && !animator.GetBool("IsWall"))
            {
                velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
                transform.position += velocity * speed * Time.deltaTime;
            }
            else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && animator.GetBool("IsWall") && IsGrounded)
            {
                velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
                transform.position += velocity * speed * Time.deltaTime;

            }
            else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D) && animator.GetBool("IsWall") && YonDuvar == 1 && Input.GetKeyDown(KeyCode.N))
            {

            }
        }
        else { animator.SetBool("IsRun", false); }
        if ((Mathf.Abs(Input.GetAxis("Horizontal")) == 0 || !IsGrounded))
        {
            animator.SetBool("IsRun", false);
        }
    }

  
    void CharacterRotation()
    {

        if (Input.GetAxis("Horizontal") < 0 && (!_isWall || (!animator.GetBool("IsWall") || !animator.GetBool("IsClimb"))))
        {
            //transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            transform.localScale = new Vector3(-1, 1);
        }
        else if (Input.GetAxis("Horizontal") > 0 && (!_isWall || (!animator.GetBool("IsWall") || !animator.GetBool("IsClimb"))))
        {
            //transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.localScale = new Vector3(1, 1);
        }

    }

    private void CharacterJump()
    {

        if (Input.GetButtonDown("Jump") && jumpCount != 0 && canJump && !animator.GetBool("IsDash") && IsGrounded)
        {
            if (jumpCount == 1)
            {
                rb.AddForce(Vector3.up * JumpForce, ForceMode2D.Impulse);
                animator.SetBool("IsJump", true);
                jumpCount--;
            }

        }
    }
    void falseDashes()
    {
        animator.SetBool("DownDash", false);
        animator.SetBool("UpDash", false);
        animator.SetBool("CrossDash", false);
        animator.SetBool("VerticalDash", false);
        animator.SetBool("IsDash", false);
    }
    void falseDashes(string activeDash)
    {
        animator.SetBool("DownDash", false);
        animator.SetBool("UpDash", false);
        animator.SetBool("CrossDash", false);
        animator.SetBool("VerticalDash", false);
        animator.SetBool("IsDash", true);
        animator.SetBool(activeDash, true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            IsGrounded = true;
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFall", false);
            sayac = 1;

            animator.SetBool("IsClimb", false);
        }
        if (collision.gameObject.tag == "wall")
        {
            _isWall = true;
        }


    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {
            jumpCount = 1;
            sayac = 1;
            animator.SetBool("IsJump", false);
            IsGrounded = true;
            animator.SetBool("IsFall", false);

        }


        if (collision.gameObject.tag == "wall")
        {
            animator.SetBool("IsJump", false);
            _isWall = true;
        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform")
        {

            if (!animator.GetBool("IsDash"))
            {
                jumpCount--;

            }//bu ne.
            IsGrounded = false;
        }

        if (collision.gameObject.tag == "wall")
        {

            canJump = true;
            _isWall = false;
            animator.SetBool("IsWall", false);
            animator.SetBool("IsClimb", false);
            rb.gravityScale = 1;
            jumpCount = 1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.tag == "end"))
        {
            end = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Event")
        {
            if (collision.gameObject.tag == "Event")
            {
                for (float i = 2; i < 3; i = i + (Time.deltaTime) / 4)
                {
                    cam.orthographicSize = i;
                }
            }
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Event")
        {
            for (float i = 4; i > 2; i = i - 0.001f)
            {
                cam.orthographicSize = i;
            }
        }
    }
    void DashAt()
    {

        
        if ((animator.GetBool("IsDash") && sayac == 1))
        {
            dashBitiren = 1;
            
            rb.totalForce = new Vector3(0, 0, 0);
            animator.SetBool("DashGravity", false);
            if (animator.GetBool("VerticalDash") && /*this.transform.rotation == Quaternion.Euler(0, 0, 0)*/transform.localScale == new Vector3(1, 1))
            {

                this.transform.position = new Vector3(this.transform.position.x + 0.04f, this.transform.position.y, this.transform.position.z);
                rb.gravityScale = 0;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = new Vector3(0, 0, 0);
            }
            else if (animator.GetBool("VerticalDash") && /*this.transform.rotation == Quaternion.Euler(0, 180, 0)*/transform.localScale == new Vector3(-1, 1))
            {

                this.transform.position = new Vector3(this.transform.position.x - 0.04f, this.transform.position.y, this.transform.position.z);
                rb.gravityScale = 0;
                rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                rb.velocity = new Vector3(0, 0, 0);
            }
            else if (animator.GetBool("UpDash"))
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.064f, this.transform.position.z);
                rb.gravityScale = 0;
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
                rb.velocity = new Vector3(0, 0, 0);
            }
            else if (animator.GetBool("DownDash") && !IsGrounded)
            {

                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.064f, this.transform.position.z);
                rb.constraints = RigidbodyConstraints2D.FreezePositionX;
       
            }
            else if (animator.GetBool("CrossDash") && Input.GetKey(KeyCode.W) && /*this.transform.rotation == Quaternion.Euler(0, 0, 0)*/transform.localScale == new Vector3(1, 1))
            {

                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.064f, this.transform.position.z);
                this.transform.position = new Vector3(this.transform.position.x + 0.04f, this.transform.position.y, this.transform.position.z);
                rb.gravityScale = 0;
                rb.velocity = new Vector3(0, 0, 0);
            }
            else if (animator.GetBool("CrossDash") && Input.GetKey(KeyCode.S) && /*this.transform.rotation == Quaternion.Euler(0, 0, 0)*/transform.localScale == new Vector3(1, 1))
            {

                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.064f, this.transform.position.z);
                this.transform.position = new Vector3(this.transform.position.x + 0.04f, this.transform.position.y, this.transform.position.z);
                rb.gravityScale = 0;
                rb.velocity = new Vector3(0, 0, 0);
            }
            else if (animator.GetBool("CrossDash") && Input.GetKey(KeyCode.W) &&/*this.transform.rotation == Quaternion.Euler(0, 180, 0)*/transform.localScale == new Vector3(-1, 1))
            {

                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.064f, this.transform.position.z);
                this.transform.position = new Vector3(this.transform.position.x - 0.04f, this.transform.position.y, this.transform.position.z);
                rb.gravityScale = 0;
                rb.velocity = new Vector3(0, 0, 0);
            }
            else if (animator.GetBool("CrossDash") && Input.GetKey(KeyCode.S) && /*this.transform.rotation == Quaternion.Euler(0, 180, 0)*/transform.localScale == new Vector3(-1, 1))
            {

                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.064f, this.transform.position.z);
                this.transform.position = new Vector3(this.transform.position.x - 0.04f, this.transform.position.y, this.transform.position.z);
                rb.gravityScale = 0;
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
        else if (animator.GetBool("DashGravity") && !animator.GetBool("IsWall") && dashBitiren == 1)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.gravityScale = 1;
            sayac--;
            dashBitiren = 0;

        }
    }

    public void DashAnim()
    {
        if (Input.GetKeyDown(KeyCode.M) && sayac == 1)
        {
            animator.SetBool("IsRun", false);

            if (Input.GetKey(KeyCode.D))
            {

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    animator.SetBool("UpDash", false);
                    animator.SetBool("VerticalDash", false);
                    animator.SetBool("DownDash", false);
                    animator.SetBool("CrossDash", true);
                }
                else
                {
                    animator.SetBool("DownDash", false);
                    animator.SetBool("UpDash", false);
                    animator.SetBool("CrossDash", false);
                    animator.SetBool("VerticalDash", true);

                }
                animator.SetBool("IsDash", true);
            }
            else if (Input.GetKey(KeyCode.A))
            {

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
                {
                    animator.SetBool("UpDash", false);
                    animator.SetBool("VerticalDash", false);
                    animator.SetBool("DownDash", false);
                    animator.SetBool("CrossDash", true);
                }
                else
                {
                    animator.SetBool("DownDash", false);
                    animator.SetBool("UpDash", false);
                    animator.SetBool("CrossDash", false);
                    animator.SetBool("VerticalDash", true);
                }
                animator.SetBool("IsDash", true);
            }
            else if (Input.GetKey(KeyCode.W))
            {
               
                animator.SetBool("DownDash", false);
                animator.SetBool("VerticalDash", false);
                animator.SetBool("CrossDash", false);
                animator.SetBool("UpDash", true);
                animator.SetBool("IsDash", true);

            }
            else if (Input.GetKey(KeyCode.S) && !IsGrounded)
            {
                animator.SetBool("UpDash", false);
                animator.SetBool("VerticalDash", false);
                animator.SetBool("CrossDash", false);
                animator.SetBool("DownDash", true);
                animator.SetBool("IsDash", true);
            }



        }

    }

    void fall()
    {

        if (rb.velocity.y < -0.2f && !animator.GetBool("IsDash") && !_isWall && !IsGrounded)
        {
            animator.SetBool("IsFall", true);
            animator.SetBool("IsJump", false);
        }
        else
        {
            animator.SetBool("IsFall", false);
        }
    }
    void StartCheckpoint()
    {
        if (savemanager.CheckpointLoad() != new Vector3(0, 0, 0))
        {
            transform.position = savemanager.CheckpointLoad();
        }
    }
    //eski tırmanma
    void Climbb()
    {
        Tırman();
        ZıplaDuvar();
        DuvaraYapis();  
    }
    void Tırman()
    {
        if ((_isWall) && Input.GetKey(KeyCode.N))
        {
            float moveY = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(0, moveY, 0);
            transform.Translate(moveDirection * 0.5f * speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                animator.SetBool("IsWall", false);
                animator.SetBool("IsClimb", true);
                falseDashes();
            }
            else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                animator.SetBool("IsWall", true);
                animator.SetBool("IsClimb", false);
                falseDashes();
            }
        }
    }
    void ZıplaDuvar()
    {
        if (Input.GetButtonDown("Jump") && !canJump && _isWall)
        {


            if (/*transform.rotation==Quaternion.Euler(0f,180f, 0f)*/transform.localScale.x == -1)
            {
                // transform.rotation = Quaternion.Euler(0f, transform.rotation.y - 180f, 0f);
                transform.localScale = new Vector3(1, 1);
                rb.AddForce(Vector3.up * JumpForce * 0.6f, ForceMode2D.Impulse);
                rb.AddForce(Vector3.right * JumpForce * 0.6f, ForceMode2D.Impulse);
                animator.SetBool("IsJump", true);
            }
            else if (/*transform.rotation==Quaternion.Euler(0f,0f, 0f)*/transform.localScale.x == 1)
            {
                //transform.rotation = Quaternion.Euler(0f, transform.rotation.y+180f, 0f);
                transform.localScale = new Vector3(-1, 1);
                rb.AddForce(Vector3.up * JumpForce * 0.6f, ForceMode2D.Impulse);
                rb.AddForce(Vector3.left * JumpForce * 0.6f, ForceMode2D.Impulse);
                animator.SetBool("IsJump", true);
            }
            jumpCount--;
        }
    }
    void DuvaraYapis()
    {
        if (_isWall && Input.GetKeyDown(KeyCode.N))
        {
            canJump = false;
            if (back.touching)
            {
                //transform.rotation = Quaternion.Euler(0f, transform.rotation.y+180f, 0f);
                transform.localScale = new Vector3(transform.localScale.x * -1, 1);
            }
            rb.velocity = Vector3.zero;
            jumpCount = 1;
            rb.gravityScale = 0f;
            animator.SetBool("IsFall", false);
            animator.SetBool("IsJump", false);
            animator.SetBool("IsWall", true);
            falseDashes();

        }
        else if (Input.GetKeyUp(KeyCode.N))

        {
            _isWall = false;
            animator.SetBool("IsWall", false);
            rb.gravityScale = 1f;
        }
    }
    void DuvardanDus()
    {
        if (_isWall && !IsGrounded && Input.GetKey(KeyCode.N))
        {
            rb.gravityScale += 0.0001f;
        }
        else if (_isWall && Input.GetKeyUp(KeyCode.N))
        {
            rb.gravityScale = 1;
        }
    }
    //buraya kadar

    //Yeni tırmanma
    void Climb()
    {
        if (Input.GetKey(KeyCode.N) && _isWall)
        {
            canJump = false;
            if (back.touching)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, 1);
            }
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            
            falseDashes();
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFall", false);
            float moveY = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(0, moveY, 0);
            transform.Translate(moveDirection * 0.5f * speed * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                animator.SetBool("IsWall", false);
                animator.SetBool("IsClimb", true);
            }
            else
            {
                animator.SetBool("IsWall", true);
                animator.SetBool("IsClimb", false);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (transform.localScale.x == -1)
                {
                    transform.localScale = new Vector3(1, 1);
                    rb.AddForce(Vector3.up * JumpForce * 0.6f, ForceMode2D.Impulse);
                    rb.AddForce(Vector3.right * JumpForce * 0.6f, ForceMode2D.Impulse);
                    animator.SetBool("IsJump", true);
                }
                else if (transform.localScale.x == 1)
                {
                    transform.localScale = new Vector3(-1, 1);
                    rb.AddForce(Vector3.up * JumpForce * 0.6f, ForceMode2D.Impulse);
                    rb.AddForce(Vector3.left * JumpForce * 0.6f, ForceMode2D.Impulse);
                    animator.SetBool("IsJump", true);
                }
                jumpCount--;
            }
        }
        else if(!_isWall || Input.GetKeyUp(KeyCode.N)) 
        {
            animator.SetBool("IsWall", false);
            animator.SetBool("IsClimb", false);
            rb.gravityScale = 1;
        }
    }
    void FallWall()
    {
        if (_isWall && !IsGrounded && Input.GetKey(KeyCode.N))
        {
            rb.gravityScale += 0.0001f;
        }
        else if (_isWall && Input.GetKeyUp(KeyCode.N))
        {
            rb.gravityScale = 1;
        }
    }
    void EkDashControl()
    {
        
            if (ekDash == 1 && sayac == 0)
            {
                sayac += 1;
                ekDash -= 1;
            }
        if (IsGrounded == true)
        {
            ekDash = 0;
        }
    }
}


