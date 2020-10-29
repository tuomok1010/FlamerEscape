using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject weaponObject;
    [SerializeField] GameObject playerAttachPoint;
    [SerializeField] GameObject weaponMuzzleParticle;

    Weapon weapon;
    ParticleSystem muzzleEffect;
    bool firstShot;                 // first shot will be shot instantly
    float timeElapsed;              // time between shots

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

        firstShot = true;
        timeElapsed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

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
            // do something if player tries to shoot without ammo?
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
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= interval || firstShot)
        {
            if(!muzzleEffect.isPlaying || firstShot) // || firstShot allows player to start shooting again before flame animation has ended
            {
                muzzleEffect.Play();
            }

            --weapon.currentAmmo;

            if (firstShot)
                firstShot = false;

            timeElapsed = 0.0f;
        }
    }

    public void StopShooting()
    {
        muzzleEffect.Stop();
        firstShot = true;
    }
}
