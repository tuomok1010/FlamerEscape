using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // TODO make a magazine and implement reloading?
    [SerializeField] public int fireRate;
    [SerializeField] public int maxAmmo;
    [SerializeField] public int startingAmmo;
    [SerializeField] public bool infiniteAmmo;

    public int currentAmmo;
    public Vector3 bulletSpawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = startingAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
