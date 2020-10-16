using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flammable : MonoBehaviour
{
    Light pointLight;
    ParticleSystem flames;
    bool isBurning;
    bool hasBurned;

    // Start is called before the first frame update
    void Start()
    {
        pointLight = transform.GetChild(0).GetComponent<Light>();
        if (pointLight)
        {
            pointLight.enabled = false;
            isBurning = false;
            hasBurned = false;
        }
        else
        {
            Debug.Log("Error! Could not find point light on " + gameObject.name);
        }

        flames = transform.GetChild(1).GetComponent<ParticleSystem>();
        if(flames)
        {
            flames.Stop();
        }
        else
        {
            Debug.Log("Error! Could not find particle system on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!flames.isPlaying && isBurning)
        {
            pointLight.enabled = false;
            isBurning = false;
            hasBurned = true;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Fire" && hasBurned == false && isBurning == false)
        {
            isBurning = true;
            pointLight.enabled = true;
            flames.Play();
        }
    }
}
