﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Flammable : MonoBehaviour
{
    [SerializeField] bool isBurningInitially;

    Light pointLight;
    Animator lightAnim;
    ParticleSystem flames;
    ParticleSystem fire;
    ParticleSystem glow;
    AudioSource burnSound;
    GameObject ashes;

    public bool isBurning { get; set; }
    public bool hasBurned { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // NOTE point light must be the 0th child of the object!
        pointLight = transform.GetChild(0).GetComponent<Light>();
        lightAnim = transform.GetChild(0).GetComponent<Animator>();   
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
        fire = transform.GetChild(2).GetComponent<ParticleSystem>();
        glow = transform.GetChild(3).GetComponent<ParticleSystem>();
        if(flames)
        {
            if(isBurningInitially)
            {
                flames.Play();
                fire.Play();
                glow.Play();
            }
            else
            {
                flames.Stop();
                fire.Stop();
                glow.Stop();
            }
        }
        
        else
        {
            Debug.Log("Error! Could not find all particle systems on " + gameObject.name);
        }

        // NOTE: Audio source must be the 4nd child of the object!
        burnSound = transform.GetChild(4).GetComponent<AudioSource>();
        if(burnSound)
        {
            if(isBurningInitially)
            {
                burnSound.enabled = true;
                burnSound.Play();
            }
            else
            {
                burnSound.Stop();
                burnSound.enabled = false;
            }
        }
        else
        {
            Debug.Log("Error! Could not find audio source on " + gameObject.name);
        }

        // NOTE: Ashes gameobject must be the 5nd child of the object!
        ashes = transform.GetChild(5).gameObject;
        if(ashes)
        {
            ashes.SetActive(false);
        }
        if(!ashes)
        {
            Debug.Log("Error! Could not find ashes gameobject on " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if the particle animation has ended and the fire is burning, turn if off
        if(!flames.isPlaying && isBurning)
        {
            isBurning = false;
            hasBurned = true;

            // change the visible model to ashes and disable collision
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            ashes.SetActive(true);
        }

        if(hasBurned)
        {
            pointLight.enabled = false;
            burnSound.Stop();
            burnSound.enabled = false;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if ((other.tag == "Fire" || other.GetComponent<ParticleSystem>().tag == "Fire") && hasBurned == false && isBurning == false)
        {
            isBurning = true;
            pointLight.enabled = true;
            lightAnim.Play("LightIgnition");
            burnSound.enabled = true;

            flames.Play();
            fire.Play();
            glow.Play();
            burnSound.Play();
            // Debug.Log("Particle collision between " + name + " and " + other.name);
        }
    }
}
