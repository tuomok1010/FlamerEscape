using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: consider moving player stats (health, isDead etc.) to the GameManager, maybe in a separate Player class

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

    AudioSource deathSound;

    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
        isDead = false;

        isInSafeZone = false;

        // TODO: see if there is a cleaner way to do this
        Component[] audioComponents = GetComponentsInChildren<AudioSource>();
        for (int i = 0; i < audioComponents.Length; ++i)
        {
            if (audioComponents[i].gameObject.tag == "PlayerDeathSound")
                deathSound = (AudioSource)audioComponents[i];
        }

        if (deathSound)
        {
            deathSound.loop = false;
            deathSound.Stop();
            deathSound.enabled = true;
            deathSound.ignoreListenerPause = true;
        }
        else
        {
            Debug.Log("Error! Could not find death sound AudioSource on player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameState != GameManager.State.GAME)
            return;

        if (isInSafeZone)
        {
            HealOverTime();
            UIController.StopDamageEffect();
        }
        else if(!isDead)
        {
            TakeDamageOverTime();
        }

        UIController.UpdatePlayerHealth(health);
        UIController.UpdateIsInSafeZone(isInSafeZone);
        UIController.UpdateIsPlayerDead(isDead);

        // NOTE: this needs to be here otherwise player will keep flashing between safe and danger zones when they are in a safezone.
        // TODO: find a better solution
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
        // NOTE: this may be buggy
        // TODO: test!
        float interval = 1.0f / damageInDarknessPerSecond;
        timeElapsedInUpdate += Time.deltaTime;

        if(timeElapsedInUpdate >= interval)
        {
            --health;
            UIController.ApplyDamageEffect();

            if (health <= 0.0f)
            {
                isDead = true;
                deathSound.Play();
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
