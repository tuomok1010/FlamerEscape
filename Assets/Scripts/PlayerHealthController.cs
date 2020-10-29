using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: consider moving player stats (health, isDead etc.) to the GameManager

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] float damageInDarknessPerSecond;
    [SerializeField] float healthRegenPerSecond;
    [SerializeField] float maxHealth;
    [SerializeField] float startingHealth;

    public bool isInSafeZone { get; set; }
    float health;
    bool isDead;

    float timeElapsedInHealOverTime = 0.0f;
    float timeElapsedInUpdate = 0.0f;

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
        if (isInSafeZone)
        {
            HealOverTime();
        }
        else if(!isDead)
        {
            TakeDamageOverTime();
        }

        UIController.UpdatePlayerHealth(health);
        UIController.UpdateIsInSafeZone(isInSafeZone);
        UIController.UpdateIsPlayerDead(isDead);

        float interval = 0.5f;
        timeElapsedInUpdate += Time.deltaTime;
        if (timeElapsedInUpdate >= interval)
        {
            isInSafeZone = false;
            timeElapsedInUpdate = 0.0f;
        }
    }

    void TakeDamageOverTime()
    {
        float interval = 1.0f / damageInDarknessPerSecond;
        timeElapsedInUpdate += Time.deltaTime;

        if(timeElapsedInUpdate >= interval)
        {
            --health;

            if (health <= 0.0f)
            {
                isDead = true;
                GameManager.gameState = GameManager.State.DEATH;
            }
            timeElapsedInUpdate = 0.0f;
        }
    }

    void HealOverTime()
    {
        float interval = 1.0f / healthRegenPerSecond;
        timeElapsedInHealOverTime += Time.deltaTime;

        if (timeElapsedInHealOverTime >= interval)
        {
            ++health;

            if (health >= maxHealth)
            {
                health = maxHealth;
            }
            timeElapsedInHealOverTime = 0.0f;
        }
    }
}
