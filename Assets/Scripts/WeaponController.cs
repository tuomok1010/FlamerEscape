using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    // TODO make weapon a separate class?
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject attachPoint;
    [SerializeField] ParticleSystem weaponParticle;
    [SerializeField] GameObject Reticle;
    [SerializeField] int fireRate;
    [SerializeField] int maxAmmo;
    [SerializeField] int startingAmmo;
    [SerializeField] bool infiniteAmmo;

    int currentAmmo;
    Vector3 particleSpawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = startingAmmo;
        if(weapon && attachPoint)
        {
            // TODO Only ignore collisions with player?
            weapon.GetComponent<BoxCollider>().enabled = false;
            weapon = Instantiate(weapon, attachPoint.transform.position, Quaternion.identity, gameObject.transform);
        }
        else
        {
            Debug.Log("Error! Weapon or attach point not selected");
        }

        if(weaponParticle)
        {
            weaponParticle.Stop();
            particleSpawnLocation = weapon.transform.GetChild(0).position;
            weaponParticle.transform.SetPositionAndRotation(particleSpawnLocation, Quaternion.identity);
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
            Shoot(ref currentAmmo, fireRate);
        }
        else
        {
            StopShooting();
        }
    }

    void Shoot(ref int currentAmmo, int fireRate)
    {
        Debug.Log("Player shoots!!");
        Debug.Log("Current fuel: " + currentAmmo);

        if (currentAmmo > 0 || infiniteAmmo)
        {
            // TODO make this more complex?
            currentAmmo -= fireRate;
            if (currentAmmo < 0)
                currentAmmo = 0;

            weaponParticle.Play();
        }
        else
        {
            Debug.Log("Out of fuel!");
        }
    }

    void StopShooting()
    {
        weaponParticle.Stop();
    }
}
