using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // in case someone wants to add more weapons in the future
    public enum WeaponType
    {
        FLAMER
    };

    // TODO make a magazine and implement reloading?
    [SerializeField] public int shotsPerMinute;
    [SerializeField] public int maxAmmo;
    [SerializeField] public int startingAmmo;
    [SerializeField] public bool infiniteAmmo;
    [SerializeField] public WeaponType weaponType;

    public int currentAmmo { get; set; }
    public Vector3 bulletSpawnLocation { get; set; }

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
