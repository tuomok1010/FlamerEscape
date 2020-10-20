using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] float damagePerSecond;
    [SerializeField] float maxHealth;
    [SerializeField] float startingHealth;

    public bool isInSafeZone { get; set; }
    float health;
    bool isDead;

    const float second = 1.0f;
    float timeElapsed = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
        isDead = false;

        isInSafeZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isInSafeZone && !isDead)
        {
            TakeDamageOverTime();
        }
    }

    void TakeDamageOverTime()
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= 1.0f)
        {
            health -= damagePerSecond;

            if(health <= 0.0f)
            {
                isDead = true;
                if(isDead)
                {
                    Debug.Log("Player died!!!");
                }
            }

            timeElapsed = 0.0f;

            Debug.Log("Player took " + damagePerSecond + " damage. Current health is " + health);
        }
    }
}
