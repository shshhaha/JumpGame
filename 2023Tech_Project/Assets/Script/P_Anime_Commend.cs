using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
public class P_Anime_Commend : MonoBehaviour
{
    public static void playerIdle(){
        Animator animator = GameObject.Find("Player").GetComponent<Animator>();
        animator.SetBool("Run", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Landing", false);
        animator.SetBool("Falling", false);
        Debug.Log("대기");
    }
    public static void playerRun(){
        Animator animator = GameObject.Find("Player").GetComponent<Animator>();
        animator.SetBool("Run", true);
        animator.SetBool("Jumping", false);
        animator.SetBool("Landing", false);
        animator.SetBool("Falling", false);
        Debug.Log("달리기");
    }
    

    //-------------------Jump-------------------
    public static void playerJump(){
        Animator animator = GameObject.Find("Player").GetComponent<Animator>();
        animator.SetBool("Jumping", true);
        Debug.Log("점프");
    }
    public static void playerFalling(){
        Animator animator = GameObject.Find("Player").GetComponent<Animator>();
        animator.SetBool("Falling", true);
        animator.SetBool("Jumping", false);
        Debug.Log("낙하");
    }
    /* public static void playerLanding(){
        Animator animator = GameObject.Find("Player").GetComponent<Animator>();
        animator.SetBool("Falling", false);
        animator.SetBool("Jumping", false);
        animator.SetBool("Landing", true);
        Debug.Log("착지");
    } */

    public static void textFade(){
        Animator animator = GameObject.Find("TAP_TO_START").GetComponent<Animator>();
        animator.SetBool("FadeStart", true);
    }
}
