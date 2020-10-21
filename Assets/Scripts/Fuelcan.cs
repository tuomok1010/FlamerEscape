using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuelcan : MonoBehaviour
{
    [SerializeField] int refillAmount;

    ParticleSystem pickupEffect;
    bool isPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        isPickedUp = false;
        pickupEffect = GetComponentInChildren<ParticleSystem>();
        if(pickupEffect)
        {
            pickupEffect.Stop();
        }
        else
        {
            Debug.Log("Could not find fuel pickup particle effect on " + name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isPickedUp && !pickupEffect.isPlaying)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player picked up " + name);
            Weapon playerWeapon = other.gameObject.GetComponentInChildren<Weapon>();

            if (playerWeapon)
            {
                RefillWeapon(ref playerWeapon);
            }
            else
            {
                Debug.Log("Failed to retrieve player weapon");
            }

            isPickedUp = true;
            GetComponent<MeshRenderer>().enabled = false;
            pickupEffect.Play();
        }
    }

    void RefillWeapon(ref Weapon weapon)
    {
        if(weapon.currentAmmo + refillAmount >= weapon.maxAmmo)
        {
            weapon.currentAmmo = weapon.maxAmmo;
        }
        else
        {
            weapon.currentAmmo += refillAmount;
        }

        Debug.Log("Player weapon refilled with " + refillAmount + " ammo.");
        Debug.Log("Current ammo: " + weapon.currentAmmo);
    }
}
