using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public GameObject player;

    void Awake() 
    {
        if (player == null)
        player = GameObject.FindWithTag("Player");
    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        // transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
        // Camera.main.transform.rotation * Vector3.up);
        transform.LookAt(player.transform.position);
    }
}
