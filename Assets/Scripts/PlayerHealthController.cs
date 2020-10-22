using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] float damageInDarknessPerSecond;
    [SerializeField] float maxHealth;
    [SerializeField] float startingHealth;

    public bool isInSafeZone { get; set; }
    float health;
    bool isDead;

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

        UIController.UpdatePlayerHealth(health);
        UIController.UpdateIsInSafeZone(isInSafeZone);
        UIController.UpdateIsPlayerDead(isDead);
    }

    void TakeDamageOverTime()
    {
        float interval = 1.0f / damageInDarknessPerSecond;
        timeElapsed += Time.deltaTime;

        if(timeElapsed >= interval)
        {
            --health;

            if (health <= 0.0f)
            {
                isDead = true;
                GameState.gameState = GameState.State.DEATH;
            }

            timeElapsed = 0.0f;
        }
    }
}
