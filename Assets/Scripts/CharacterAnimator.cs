using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
	
	
	public Animator animator;
	public GameObject Crosshair;
	

	float moveThreshold = 0.05f;
	
	Vector2 movement;
	Vector3 facing;
	
	
    void Update()
    {
        // movement.x = Input.GetAxisRaw("Horizontal");
		// movement.y = Input.GetAxisRaw("Vertical");

		if(movement.sqrMagnitude > moveThreshold)
		{
			animator.SetBool("Moving", true);
		}
		else
		{
			animator.SetBool("Moving", false);
		}
	
		FaceCrosshair();

		animator.SetFloat("Horizontal", movement.x);
		animator.SetFloat("Vertical", movement.y);
		animator.SetFloat("Speed", movement.sqrMagnitude);
    }
		void FaceCrosshair()
	{
		Vector3 dir = (this.transform.position - Crosshair.transform.position).normalized;
		Debug.DrawLine (this.transform.position, this.transform.position - dir * 10, Color.red, Mathf.Infinity);
		Debug.Log(dir);
	}
	
}
