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
                ClearAll();
            } break;
        }
    }

    void DrawMenuUI()
    {
        gameNameText.color = textWhite;
        gameNameText.text = "FLAMER";

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
    }

    void DrawWinUI()
    {
        isPlayerDeadText.color = textGreen;
        isPlayerDeadText.text = "YOU SURVIVED";

        pressSpaceText.color = textWhite;
        pressSpaceText.text = "Press space to play again";

        pressEscText.color = textWhite;
        pressEscText.text = "Press esc to quit";
    }

    void DrawPauseUI()
    {
        gameNameText.color = textWhite;
        gameNameText.text = "PAUSED";

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
