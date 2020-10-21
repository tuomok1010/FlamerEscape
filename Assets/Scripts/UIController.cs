using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text fuelText;
    [SerializeField] Text healthText;

    static int flamerFuel;
    static float health;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        fuelText.text = "Fuel: " + flamerFuel;
        healthText.text = "Health: " + health;
    }

    static public void UpdateFlamerFuel(int newAmount)
    {
        flamerFuel = newAmount;
    }

    static public void UpdatePlayerHealth(float newAmount)
    {
        health = newAmount;
    }
}
