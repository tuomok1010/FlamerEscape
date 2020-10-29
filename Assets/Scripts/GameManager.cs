using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        MENU,
        GAME,
        DEATH,
        WIN,
        PAUSE
    }

    public static State gameState;

    private void Awake()
    {
        gameState = GameManager.State.MENU;
        SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // TODO: test and see if these work
    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public static void UnpauseGame()
    {
        Time.timeScale = 1;
    }

    public static void RestartGame()
    {
        gameState = GameManager.State.MENU;
        SceneManager.LoadScene(0);
    }
}
