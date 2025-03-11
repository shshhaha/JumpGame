using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class P_Main : MonoBehaviour
{
    private P_Commend p_commend;
    private F_PlayerCamera F_camera;
    void Start()
    {
        p_commend = FindObjectOfType<P_Commend>();
        F_camera = FindObjectOfType<F_PlayerCamera>();
    }

    void FixedUpdate()
    {
        p_commend.PlayerRaycast();
        p_commend.IsGole();
        p_commend.PlayerLocationControl();
        p_commend.PlayerRotationControl();  
    }

}