using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    TODO: Too messy. Try and optimize this and make it look cleaner. Also consider simplifying as there is no need to support additional weapon types.
*/

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject weaponObject;
    [SerializeField] GameObject playerAttachPoint;
    [SerializeField] GameObject weaponMuzzleParticle;

    Weapon weapon;
    ParticleSystem muzzleEffect;    // this basically is the flamer flames
    AudioSource weaponSound;
    bool firstShot;                 // first shot will be shot instantly
    float timeElapsedBetweenShots;
    bool startFade;                 // flag used to enable fading the flamer weaponsound so that it doesn't clip to a complete stop suddenly
    float startVolume;

    const float weaponSoundFadeTimeInSeconds = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(weaponObject && playerAttachPoint)
        {
            // TODO Only ignore collisions with player?
            weaponObject.GetComponent<BoxCollider>().enabled = false;
            weaponObject = Instantiate(weaponObject, playerAttachPoint.transform.position, Quaternion.identity, gameObject.transform);
            weapon = weaponObject.GetComponent<Weapon>();
            weapon.bulletSpawnLocation = weapon.transform.GetChild(0).position;
        }
        else
        {
            Debug.Log("Error! Weapon or attach point not selected");
        }

        if(weaponMuzzleParticle)
        {
            weaponMuzzleParticle = Instantiate(weaponMuzzleParticle, weapon.bulletSpawnLocation, Quaternion.identity, weaponObject.transform);
            muzzleEffect = weaponMuzzleParticle.GetComponent<ParticleSystem>();
            muzzleEffect.Stop();
        }
        else
        {
            Debug.Log("Error! weapon particle not selected");
        }

        // TODO: see if there is a cleaner way to do this
        Component [] audioComponents = GetComponentsInChildren<AudioSource>();
        for(int i = 0; i < audioComponents.Length; ++i)
        {
            if (audioComponents[i].gameObject.tag == "WeaponSound")
                weaponSound = (AudioSource)audioComponents[i];
        }

        if(weaponSound)
        {
            weaponSound.Stop();
            if (weapon.weaponType == Weapon.WeaponType.FLAMER)
                weaponSound.loop = true;
            else
                weaponSound.loop = false;
        }
        else
        {
            Debug.Log("Error! Could not find AudioSource for weapon");
        }

        firstShot = true;
        timeElapsedBetweenShots = 0.0f;
        startFade = false;
        startVolume = weaponSound.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (startFade)
        {
            FadeAudio();
        }
    }

    public void Shoot()
    {
        // Debug.Log("Player shoots!!");
        // Debug.Log("Current fuel: " + weapon.currentAmmo);

        if (weapon.currentAmmo > 0 || weapon.infiniteAmmo)
        {
            switch(weapon.weaponType)
            {
                case Weapon.WeaponType.FLAMER:
                {
                    FlamerFire();
                } break;
            }
        }
        else
        {
            switch (weapon.weaponType)
            {
                case Weapon.WeaponType.FLAMER:
                {
                    StopShooting();
                }
                break;
            }
        }

        UIController.UpdateFlamerFuel(weapon.currentAmmo);
    }

    void FlamerFire()
    {
        if (weapon.currentAmmo < 0)
        {
            weapon.currentAmmo = 0;
        }

        float interval = 60.0f / weapon.shotsPerMinute;
        timeElapsedBetweenShots += Time.deltaTime;

        if (timeElapsedBetweenShots >= interval || firstShot)
        {
            if(!muzzleEffect.isPlaying || firstShot) // || firstShot allows player to start shooting again before flame animation has ended
            {
                muzzleEffect.Play();
            }
            if(!weaponSound.isPlaying || firstShot)
            {
                weaponSound.Play();
            }

            --weapon.currentAmmo;

            if (firstShot)
                firstShot = false;

            timeElapsedBetweenShots = 0.0f;
        }
    }

    public void StopShooting()
    {
        muzzleEffect.Stop();
        firstShot = true;
        startFade = true;
    }

    void FadeAudio()
    {
        weaponSound.volume -= startVolume * Time.deltaTime / weaponSoundFadeTimeInSeconds;

        if (weaponSound.volume <= 0.0f)
        {
            weaponSound.Stop();
            weaponSound.volume = startVolume;
            startFade = false;
        }
    }
}

    
