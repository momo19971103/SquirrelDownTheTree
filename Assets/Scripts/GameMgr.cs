using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMgr : MonoBehaviour
{
    public Button restartBtn;
    public GameObject player;
    void Start()
    {
        restartBtn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayMgr.isDead) {
            PlayMgr.ResetAnimation();
            PlayMgr.animator.SetBool("Dead",true);
            PlayMgr.isDead = false;
            StartCoroutine("delayDisplay");
            restartBtn.gameObject.SetActive(true);
        }   
    }
    IEnumerator delayDisplay()
    {
        yield return new WaitForSeconds((float)0.5);
        player.SetActive(false);
    }
    public void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
