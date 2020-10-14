using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum Orientation
    {
        LEFT, RIGHT, UP, DOWN, DIAGONAL_UP_RIGHT, DIAGONAL_UP_LEFT, DIAGONAL_DOWN_RIGHT, DIAGONAL_DOWN_LEFT
    }

    [SerializeField] float movementSpeed;
    [SerializeField] float ammoConsumeRate;
    [SerializeField] float maxAmmo;
    [SerializeField] float startingAmmo;

    Rigidbody2D rb;
    GameObject weapon;
    float vAxis;
    float hAxis;
    Orientation orientation;

    float currentAmmo;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = startingAmmo;
        orientation = Orientation.RIGHT;

        rb = GetComponent<Rigidbody2D>();
        if(!rb)
        {
            Debug.Log("Error! Player rigidbody not found");
        }

        weapon = GameObject.FindGameObjectWithTag("Weapon");
        if (!weapon)
        {
            Debug.Log("Error! Player weapon with Tag \"Weapon\" not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");
        orientation = SetPlayerOrientation(vAxis, hAxis);

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Player shoots!!");
            Debug.Log("Current fuel: " + currentAmmo);

            if (currentAmmo > 0.0f)
            {
                currentAmmo -= ammoConsumeRate;
            }
            else
            {
                Debug.Log("Out of fuel!");
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(hAxis * movementSpeed, vAxis * movementSpeed);

        switch(orientation)
        {
            case Orientation.LEFT:
            {
                
            } break;
            case Orientation.RIGHT:
            {
                
            } break;
            case Orientation.UP:
            {

            } break;
            case Orientation.DOWN:
            {

            } break;
            case Orientation.DIAGONAL_UP_RIGHT:
            {

            } break;
            case Orientation.DIAGONAL_UP_LEFT:
            {

            } break;
            case Orientation.DIAGONAL_DOWN_RIGHT:
            {

            } break;
            case Orientation.DIAGONAL_DOWN_LEFT:
            {

            } break;
        }
    }

    Orientation SetPlayerOrientation(float vAxis, float hAxis)
    {
        Orientation orientation = Orientation.RIGHT;

        if (hAxis > 0 && vAxis > 0)
        {
            Debug.Log("Player orientation: upper right");
            orientation = Orientation.DIAGONAL_UP_RIGHT;
        }
        else if(hAxis < 0 && vAxis > 0)
        {
            Debug.Log("Player orientation: upper left");
            orientation = Orientation.DIAGONAL_UP_LEFT;
        }
        else if (hAxis > 0 && vAxis < 0)
        {
            Debug.Log("Player orientation: lower right");
            orientation = Orientation.DIAGONAL_DOWN_RIGHT;
        }
        else if (hAxis < 0 && vAxis < 0)
        {
            Debug.Log("Player orientation: lower left");
            orientation = Orientation.DIAGONAL_DOWN_LEFT;
        }
        else if (hAxis > 0)
        {
            Debug.Log("Player orientation: right");
            orientation = Orientation.RIGHT;
        }
        else if (hAxis < 0)
        {
            Debug.Log("Player orientation: left");
            orientation = Orientation.LEFT;
        }
        else if (vAxis > 0)
        {
            Debug.Log("Player orientation: up");
            orientation = Orientation.UP;
        }
        else if (vAxis < 0)
        {
            Debug.Log("Player orientation: down");
            orientation = Orientation.DOWN;
        }

        return orientation;
    }
}
