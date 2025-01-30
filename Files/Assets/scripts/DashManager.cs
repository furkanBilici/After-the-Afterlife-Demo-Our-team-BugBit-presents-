using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DashManager : StateMachineBehaviour
{
    


    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DashControll(stateInfo,animator);
    }

    void DashControll( AnimatorStateInfo stateInfo, Animator animator)
    {
        float normalizedTime = stateInfo.normalizedTime % 1;
        if (normalizedTime >= 0.90f)
        {
            animator.SetBool("IsDash", false);
            animator.SetBool("VerticalDash", false);
            animator.SetBool("CrossDash", false);
            animator.SetBool("UpDash", false);
            animator.SetBool("DownDash", false);
            animator.SetBool("DashGravity", true);
        }
    }
}
