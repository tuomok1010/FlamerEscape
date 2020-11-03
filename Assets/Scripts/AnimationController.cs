using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if(!animator)
        {
            Debug.Log("Error! Could not find Animator on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


    }
}
