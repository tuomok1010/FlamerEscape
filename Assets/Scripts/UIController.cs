using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // TODO: rename into something better because some of these a reused with different text in them
    [SerializeField] Text fuelText;
    [SerializeField] Text healthText;
    [SerializeField] Text safeZoneText;
    [SerializeField] Text isPlayerDeadText;
    [SerializeField] Text gameNameText;
    [SerializeField] Text pressSpaceText;
    [SerializeField] Text pressEscText;
    [SerializeField] Text controls;
    [SerializeField] Text creditsText;
    [SerializeField] Text musicCreditsText;
    [SerializeField] Image deathScreen;
    [SerializeField] Image winScreen;
    [SerializeField] Image damageEffect;
    [SerializeField] Image logo;

    static int flamerFuel;
    static float health;
    static bool isInSafeZone;
    static bool isPlayerDead;

    static float damageEffectAlpha;
    static float damageEffectMaxAlpha;
    static float damageEffectMinAlpha;
    static float damageEffectAlphaIncrement;

    Color textGreen;
    Color textRed;
    Color textWhite;

    // Start is called before the first frame update
    void Start()
    {
        damageEffectMinAlpha = 0.0f;
        damageEffectAlpha = damageEffectMinAlpha;
        damageEffectMaxAlpha = 0.05f;
        damageEffectAlphaIncrement = 0.0005f;

        textGreen = new Color(0.0f, 0.5f, 0.0f, 1.0f);
        textRed = new Color(0.5f, 0.0f, 0.0f, 1.0f);
        textWhite = new Color(0.5f, 0.5f, 0.5f, 1.0f);

        ClearAll();
    }

    // Update is called once per frame
    void Update()
    {
        ClearAll();

        switch (GameManager.gameState)
        {
            case GameManager.State.MENU:
            {
                DrawMenuUI();
            } break;

            case GameManager.State.GAME:
            {
                DrawGameUI();
            } break;

            case GameManager.State.DEATH:
            {
                StopDamageEffect();
                DrawDeathUI();
            } break;

            case GameManager.State.WIN:
            {
                DrawWinUI();
            } break;

            case GameManager.State.PAUSE:
            {
                DrawPauseUI();
            } break;

            default:
            {
                Debug.Log("Error! Unknown game state!");
                ClearAll();
            } break;
        }
    }

    void DrawMenuUI()
    {
        //gameNameText.color = textWhite;
        //gameNameText.text = "FLAMER";
        logo.enabled = true;

        pressSpaceText.color = textWhite;
        pressSpaceText.text = "Press space to start";

        pressEscText.color = textWhite;
        pressEscText.text = "Press esc to quit";

        controls.color = textWhite;
        controls.text = "Move: WASD\nShoot: Space\nPause: Esc";
    }

    void DrawGameUI()
    {
        fuelText.text = "Fuel: " + flamerFuel;
        healthText.text = "Health: " + health;
        damageEffect.color = new Color(damageEffect.color.r, damageEffect.color.g, damageEffect.color.b, damageEffectAlpha);

        if (isInSafeZone)
        {
            safeZoneText.color = textGreen;
            safeZoneText.text = "Safe Zone";
        }
        else
        {
            safeZoneText.color = textRed;
            safeZoneText.text = "Danger Zone";
        }
    }

    void DrawDeathUI()
    {
        isPlayerDeadText.color = textRed;
        isPlayerDeadText.text = "YOU DIED";

        pressSpaceText.color = textWhite;
        pressSpaceText.text = "Press space to play again";

        pressEscText.color = textWhite;
        pressEscText.text = "Press esc to quit";

        deathScreen.enabled = true;
    }

    void DrawWinUI()
    {
        isPlayerDeadText.color = textGreen;
        isPlayerDeadText.text = "YOU SURVIVED";

        pressSpaceText.color = textWhite;
        pressSpaceText.text = "Press space to play again";

        pressEscText.color = textWhite;
        pressEscText.text = "Press esc to quit";

        creditsText.color = textWhite;
        creditsText.text = "Flamer is made by:\nTuomo\nPasi\nMaria\nSamu\nEetu";

        musicCreditsText.color = textWhite;
        musicCreditsText.text = "music and sound from Zapsplat";

        winScreen.enabled = true;
    }

    void DrawPauseUI()
    {
        //gameNameText.color = textWhite;
        //gameNameText.text = "PAUSED";
        logo.enabled = true;

        pressSpaceText.color = textWhite;
        pressSpaceText.text = "Press space to continue";

        pressEscText.color = textWhite;
        pressEscText.text = "Press esc to quit";

        controls.color = textWhite;
        controls.text = "Move: WASD\nShoot: Space\nPause: Esc";
    }

    void ClearAll()
    {
        fuelText.text = "";
        healthText.text = "";
        safeZoneText.text = "";
        isPlayerDeadText.text = "";
        gameNameText.text = "";
        pressSpaceText.text = "";
        pressEscText.text = "";
        controls.text = "";
        creditsText.text = "";
        musicCreditsText.text = "";
        deathScreen.enabled = false;
        winScreen.enabled = false;
        logo.enabled = false;
    }

    static public void UpdateFlamerFuel(int newValue)
    {
        flamerFuel = newValue;
    }

    static public void UpdatePlayerHealth(float newValue)
    {
        health = newValue;
    }

    static public void UpdateIsInSafeZone(bool newValue)
    {
        isInSafeZone = newValue;
    }

    static public void UpdateIsPlayerDead(bool newValue)
    {
        isPlayerDead = newValue;
    }

    static public void ApplyDamageEffect()
    {
        if (damageEffectAlpha <= damageEffectMinAlpha)
            damageEffectAlphaIncrement = 0.01f;
        else if (damageEffectAlpha >= damageEffectMaxAlpha)
            damageEffectAlphaIncrement = -0.01f;

        damageEffectAlpha += damageEffectAlphaIncrement;
    }

    static public void StopDamageEffect()
    {
        damageEffectAlpha = 0.0f;
    }

}
