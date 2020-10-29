using ECM.Controllers;
using UnityEngine;

namespace ECM.Walkthrough.CustomCharacterController
{
    /// <summary>
    /// Example of a custom character controller.
    ///
    /// This show how to create a custom character controller extending one of the included 'Base' controller. 
    /// </summary>
    ///

    public class MyCharacterController : BaseCharacterController
    {
        protected override void Animate()
        {
            // Add animator related code here...
        }

        protected override void HandleInput()
        {
            // Toggle pause / resume.
            // By default, will restore character's velocity on resume (eg: restoreVelocityOnResume = true)

            /*
            if (Input.GetKeyDown(KeyCode.P))
                pause = !pause;
            */

            switch (GameManager.gameState)
            {
                case GameManager.State.MENU:
                {
                    // FreezeCharacter();
                    GameManager.PauseGame();
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        // UnfreezeCharacter();
                        GameManager.UnpauseGame();
                        GameManager.gameState = GameManager.State.GAME;
                    }

                    if(Input.GetKeyDown(KeyCode.Escape))
                    {
                        Application.Quit();
                    }

                } break;

                case GameManager.State.GAME:
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        gameObject.GetComponent<WeaponController>().Shoot();
                    }

                    if (Input.GetKeyUp(KeyCode.Space))
                    {
                        gameObject.GetComponent<WeaponController>().StopShooting();
                    }

                    if(Input.GetKeyDown(KeyCode.Escape))
                    {
                        GameManager.gameState = GameManager.State.PAUSE;
                    }

                } break;

                case GameManager.State.DEATH:
                {
                    // FreezeCharacter();
                    GameManager.PauseGame();
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        GameManager.RestartGame();
                    }

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        Application.Quit();
                    }
                } break;

                case GameManager.State.WIN:
                {
                    // FreezeCharacter();
                    GameManager.PauseGame();
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        GameManager.RestartGame();
                    }

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        Application.Quit();
                    }

                } break;

                case GameManager.State.PAUSE:
                {
                    // FreezeCharacter();
                    GameManager.PauseGame();

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        GameManager.gameState = GameManager.State.GAME;
                        GameManager.UnpauseGame();
                    }

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        Application.Quit();
                    }

                } break;

                default:
                {

                } break;
            }

            // Handle user input

            moveDirection = new Vector3
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = 0.0f,
                z = Input.GetAxisRaw("Vertical")
            };

            // jump not needed
            // jump = Input.GetButton("Jump");

            // crouch not needed
            // crouch = Input.GetKey(KeyCode.C);
        }

        public void FreezeCharacter()
        {
            pause = true;
        }

        public void UnfreezeCharacter()
        {
            pause = false;
        }
    }
}
