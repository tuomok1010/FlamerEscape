using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour
{
    public GameObject facing_target;
    public Animator animator;

    float dir_vertical = 0.0f;
    float dir_horizontal = 0.0f;

    float speed_vertical;
    float speed_horizontal;
    float speed_threshold = 0.05f;

    void Awake()
    {
        // if (facing_target == null)
        //     facing_target = GameObject.FindWithTag("Facing_target");
    }

    // Update is called once per frame
    void Update()
    {
        //Point a vector towards crosshair reticle
        Vector3 pos = this.transform.position;
        Vector3 dir = (facing_target.transform.position - this.transform.position).normalized;
        // Debug.DrawLine (pos, pos + dir * 1, Color.red, 1f);
        
        //Convert vector direction to be usable by blend tree
		dir_horizontal = dir.z;
		dir_vertical = dir.x;

        speed_horizontal = Input.GetAxisRaw("Horizontal");
        speed_vertical = Input.GetAxisRaw("Vertical");

        //Detect if movement keys are being pressed a bit
        if(Mathf.Abs(speed_horizontal) + Mathf.Abs(speed_vertical) > speed_threshold)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        //tell animator parameters to do their thing
        animator.SetFloat("Horizontal", dir_horizontal);
        animator.SetFloat("Vertical", dir_vertical);

    }
    
    //Debugger for facing_target vector magnitude and direction
	// void OnGUI()
	// {
	// 	int w = Screen.width, h = Screen.height;

	// 	GUIStyle style = new GUIStyle();

	// 	Rect rect = new Rect(0, 20, w, h * 4 / 100);
	// 	style.alignment = TextAnchor.UpperLeft;
	// 	style.fontSize = h * 4 / 100;
	// 	style.normal.textColor = new Color (1.0f, 0.85f, 0.6f, 0.8f);

	// 	string text = string.Format("{0:0.0} x {1:0.0} y", dir_vertical, dir_horizontal);
	// 	GUI.Label(rect, text, style);
	// }
}
