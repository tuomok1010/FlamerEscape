using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject weaponObject;
    [SerializeField] GameObject playerAttachPoint;
    [SerializeField] ParticleSystem weaponParticle;
    [SerializeField] GameObject Reticle;

    Weapon weapon;


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

        if(weaponParticle)
        {
            weaponParticle.Stop();
            weaponParticle.transform.SetPositionAndRotation(weapon.bulletSpawnLocation, Quaternion.identity);
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
            Shoot(ref weapon.currentAmmo, weapon.fireRate);
        }
        else
        {
            StopShooting();
        }
    }

    void Shoot(ref int currentAmmo, int fireRate)
    {
        // Debug.Log("Player shoots!!");
        // Debug.Log("Current fuel: " + currentAmmo);

        if (currentAmmo > 0 || weapon.infiniteAmmo)
        {
            // TODO make this more complex?
            currentAmmo -= fireRate;
            if (currentAmmo < 0)
                currentAmmo = 0;

            if(!weaponParticle.isPlaying)
                weaponParticle.Play();
        }
        else
        {
            // Debug.Log("Out of fuel!");
        }
    }

    void StopShooting()
    {
        weaponParticle.Stop();
    }
}
