using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flammable : MonoBehaviour
{
    [SerializeField] bool isBurningInitially;

    Light pointLight;
    ParticleSystem flames;

    public bool isBurning { get; set; }
    public bool hasBurned { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // NOTE point light must be the 0th child of the object!
        pointLight = transform.GetChild(0).GetComponent<Light>();
        if (pointLight)
        {
            if(isBurningInitially)
            {
                pointLight.enabled = true;
                isBurning = true;
                hasBurned = false;
            }
            else
            {
                pointLight.enabled = false;
                isBurning = false;
                hasBurned = false;
            }
        }
        else
        {
            Debug.Log("Error! Could not find point light on " + gameObject.name);
        }

        // NOTE particle system must be the 1st child of the object!
        flames = transform.GetChild(1).GetComponent<ParticleSystem>();
        if(flames)
        {
            if(isBurningInitially)
            {
                flames.Play();
            }
            else
            {
                flames.Stop();
            }
        }
        else
        {
            Debug.Log("Error! Could not find particle system on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if the particle animation has ended and the fire is burning, turn if off
        if(!flames.isPlaying && isBurning)
        {
            pointLight.enabled = false;
            isBurning = false;
            hasBurned = true;
        }

        if(hasBurned)
        {
            // TODO change the model to a destoyed version
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if ((other.tag == "Fire" || other.GetComponent<ParticleSystem>().tag == "Fire") && hasBurned == false && isBurning == false)
        {
            isBurning = true;
            pointLight.enabled = true;
            flames.Play();

            // Debug.Log("Particle collision between " + name + " and " + other.name);
        }
    }
}
