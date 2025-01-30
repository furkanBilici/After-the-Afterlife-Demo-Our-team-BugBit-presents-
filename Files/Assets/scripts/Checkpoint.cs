using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{    
    public OlumManager OlumManager;
    public Animator animator;
    public SaveManager SaveManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "character")
        {
            OlumManager.checkpoint=transform.position;
            OlumManager.CanCheck = true;
            SaveManager.CheckpointSave(transform.position);
        }
    }
}
