using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text fuelText;
    [SerializeField] Text healthText;
    [SerializeField] Text safeZoneText;
    [SerializeField] Text isPlayerDeadText;
    [SerializeField] Text gameNameText;
    [SerializeField] Text pressSpaceText;

    static int flamerFuel;
    static float health;
    static bool isInSafeZone;
    static bool isPlayerDead;

    Color textGreen;
    Color textRed;
    Color textWhite;

    // Start is called before the first frame update
    void Start()
    {
        textGreen = new Color(0.0f, 0.5f, 0.0f, 1.0f);
        textRed = new Color(0.5f, 0.0f, 0.0f, 1.0f);
        textWhite = new Color(0.5f, 0.5f, 0.5f, 1.0f);

        fuelText.text = "";
        healthText.text = "";
        safeZoneText.text = "";
        isPlayerDeadText.text = "";
        gameNameText.text = "";
        pressSpaceText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
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
                DrawDeathUI();
            } break;

            default:
            {
                ClearAll();
            } break;
        }
    }

    void DrawMenuUI()
    {
        ClearAll();
        gameNameText.color = textWhite;
        gameNameText.text = "FLAMER";

        pressSpaceText.color = textWhite;
        pressSpaceText.text = "Press space to start";
    }

    void DrawGameUI()
    {
        ClearAll();
        fuelText.text = "Fuel: " + flamerFuel;
        healthText.text = "Health: " + health;

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
        ClearAll();
        isPlayerDeadText.color = textRed;
        isPlayerDeadText.text = "YOU DIED";
    }

    void ClearAll()
    {
        fuelText.text = "";
        healthText.text = "";
        safeZoneText.text = "";
        isPlayerDeadText.text = "";
        gameNameText.text = "";
        pressSpaceText.text = "";
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
}
