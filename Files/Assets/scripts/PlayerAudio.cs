using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public AudioSource dash;
    public AudioSource walk;
    public AudioSource jump;
    public AudioSource climb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Walk();
        Dash();
        Climb();
        Jump();
    }
    void Walk()
    {
        if (anim.GetBool("IsRun") && !walk.isPlaying)
        {
            walk.Play();
        }
        else if (!anim.GetBool("IsRun"))
        {
            walk.Pause();
        }
    }
    void Dash()
    {
        if (anim.GetBool("IsDash"))
        {
            dash.Play();
        }
    }
    void Jump()
    {
        if (anim.GetBool("IsJump"))
        {
            jump.Play();
        }
    }
    void Climb()
    {
        if (anim.GetBool("IsClimb"))
        {
            climb.Play();
        }
    }

}
