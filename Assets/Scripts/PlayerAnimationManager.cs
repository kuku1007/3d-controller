using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LM {
    // public class PlayerAnimationManager : AnimationManager
    public class PlayerAnimationManager : MonoBehaviour
    {
        // PlayerManager playerManager;
        // InputHandler inputHandler;
        PlayerLocomotion playerLocomotion;
    
        int horizontal;
        int vertical;
        // public bool canRotate;
        public Animator anim;

        public void Initialize() {
            // playerManager = GetComponentInParent<PlayerManager>();
            anim = GetComponent<Animator>();
            // inputHandler = GetComponentInParent<InputHandler>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void PlayTargetAnimation(string targetAnim, bool animationOngoing) {
            // anim.applyRootMotion = animationOngoing; TODO
            anim.applyRootMotion = false;
            anim.SetBool("animationOngoing", animationOngoing);
            anim.CrossFade(targetAnim, 0.2f);
        }
        
        public void UpdateAnimator(float verticalMovement, float horizontalMovement, bool isSprinting) {
            #region Vertical
            float v = 0;
            if(verticalMovement > 0 && verticalMovement < 0.55f) {
                v = 0.5f;
            } 
            else if(verticalMovement >= 0.55f ) {
                v = 1;
            }
            else if(verticalMovement < 0 && verticalMovement > -0.55f ) {
                v = -0.5f;
            }
            else if(verticalMovement <= -0.55f ) {
                v = -1f;
            } 
            else {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;
            if(horizontalMovement > 0 && horizontalMovement < 0.55f) {
                h = 0.5f;
            } 
            else if(horizontalMovement >= 0.55f ) {
                h = 1;
            }
            else if(horizontalMovement < 0 && horizontalMovement > -0.55f ) {
                h = -0.5f;
            }
            else if(horizontalMovement <= -0.55f ) {
                h = -1f;
            } 
            else {
                h = 0;
            }
            #endregion

            if(isSprinting) {
                v = 2; //TODO: h to horizontalM ???
            }

            if(v < 0 && h != 0) { // If strafe then do not walk back
                v = 0;
            } 
            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        // public void CanRotate() {
        //     canRotate = true;
        // }

        // public void StopRotation() {
        //     canRotate = false;
        // }

        public void EnableInvulnerablity() {
            anim.SetBool("isInvulnerable", true);
        }

        public void DisableInvulnerablity() {
            anim.SetBool("isInvulnerable", false);
        }

        private void OnAnimatorMove() {
            // if(playerManager.animationOngoing == false)
            //     return;
            
            // playerLocomotion.characterController.attachedRigidbody.interpolation
            // float delta = Time.deltaTime;
            // Vector3 deltaPostion = anim.deltaPosition;
            // deltaPostion.y = 0;
            // playerLocomotion.characterController.Move(deltaPostion);
        }
    
        // public void SuccessfullSpellCast() {
            
        // }
    }
}

