using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE the radius of the safeZone sphere collider should be equal to the range of the light

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
            PlayerHealthController phc = other.gameObject.GetComponent<PlayerHealthController>();
            if (phc)
            {
                if (isValid)
                {
                    // Debug.Log("Player entered safeZone");
                    phc.isInSafeZone = true;
                }
                else
                {
                    phc.isInSafeZone = false;
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
            // Debug.Log("Player exited safeZone");

            PlayerHealthController phc = other.gameObject.GetComponent<PlayerHealthController>();
            phc.isInSafeZone = false;
        }
    }
}
