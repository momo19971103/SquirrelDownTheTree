using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMgr : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
            PlayMgr.isDead = true;
          
        }
    }
}
