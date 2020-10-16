using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject weaponObject;
    [SerializeField] GameObject playerAttachPoint;
    [SerializeField] GameObject weaponMuzzleParticle;
    [SerializeField] GameObject Reticle;

    Weapon weapon;
    ParticleSystem muzzleEffect;


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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Shoot(ref weapon);
        }
    }

    void Shoot(ref Weapon weapon)
    {
        Debug.Log("Player shoots!!");
        Debug.Log("Current fuel: " + weapon.currentAmmo);

        if (weapon.currentAmmo > 0 || weapon.infiniteAmmo)
        {
            switch(weapon.weaponType)
            {
                case Weapon.WeaponType.FLAMER:
                {
                    FlamerFire(ref weapon);
                } break;
            }
        }
        else
        {
            Debug.Log("Out of ammo!");
        }
    }

    void FlamerFire(ref Weapon weapon)
    {
        if (weapon.currentAmmo < 0)
        {
            weapon.currentAmmo = 0;
        }

        if (!muzzleEffect.isPlaying)
        {
            muzzleEffect.Play();
            weapon.currentAmmo -= weapon.fireRate;
        }
    }
}
