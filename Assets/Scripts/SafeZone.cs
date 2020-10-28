using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: consider making this more generic so that it doesn't necessarily require a flammable component in parent.

// TODO: Support for other shapes in addition to the sphere

// TODO: There might be a bug when there are multiple safezones near each other. Seems to happen when you move
//       between safepoints as one of the fires fade. Find and fix!!

public class SafeZone : MonoBehaviour
{
    Flammable flammable;
    bool isValid;

    // Start is called before the first frame update
    void Start()
    {
        flammable = GetComponentInParent<Flammable>();
        if(!flammable)
        {
            Debug.Log("Error! Could not find flammable component");
        }

        // no need to render as this is only a trigger
        GetComponent<MeshRenderer>().enabled = false;

        GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<SphereCollider>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (flammable.isBurning)
        {
            isValid = true;
            // Debug.Log("set isValid to true");
        }
        else
        {
            isValid = false;
            // Debug.Log("set isValid to false");
        }

        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController p = other.gameObject.GetComponent<PlayerHealthController>();
            if (p)
            {
                if (isValid)
                {
                    // Debug.Log("Player entered safeZone");
                    p.isInSafeZone = true;
                }
                else
                {
                    p.isInSafeZone = false;
                }
            }
            else
            {
                Debug.Log("Failed to retrieve PlayerHealthComponent in SafeZone");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealthController p = other.gameObject.GetComponent<PlayerHealthController>();
            p.isInSafeZone = false;
        }
    }
}
