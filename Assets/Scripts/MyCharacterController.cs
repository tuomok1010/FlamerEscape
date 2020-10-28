using ECM.Controllers;
using UnityEngine;

namespace ECM.Walkthrough.CustomCharacterController
{
    /// <summary>
    /// Example of a custom character controller.
    ///
    /// This show how to create a custom character controller extending one of the included 'Base' controller. 
    /// </summary>

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

            // TODO: is this the best place for this?
            if (GameManager.gameState == GameManager.State.MENU)
            {
                FreezeCharacter();
                // GameManager.PauseGame(); // // TODO Use this to pause/unpause instead?
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    UnfreezeCharacter();
                    // GameManager.UnpauseGame(); // TODO Use this to pause/unpause instead?
                    GameManager.gameState = GameManager.State.GAME;
                }
            }

            if(GameManager.gameState == GameManager.State.DEATH)
            {
                FreezeCharacter();
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
