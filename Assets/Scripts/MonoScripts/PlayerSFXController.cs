using System.Collections.Generic;
using MyBox;
using Packages.Tarodev_2D_Controller._Scripts;
using SOs;
using UnityEngine;

namespace MonoScripts
{
    public class PlayerSfxController : MonoBehaviour
    {
        [SerializeField] private SoundControllerSo sfxController;
        [SerializeField] private PlayerController playerController;
        [Separator] 
        [SerializeField] private AudioClip jumpClip;
        [SerializeField] private AudioClip movementClip;
    
        [SerializeField] private AnimationCurve landingLevels;
        [SerializeField] private List<AudioClip> landingClips;

        private Rigidbody2D _playerRb;

        private void Awake()
        {
            playerController.GetComponent<Rigidbody2D>();
        
            playerController.Jumped += PlayJumpSfx;
            playerController.Moved += PlayMovementSfx;
            playerController.GroundedChanged += PlayLandingSfx;
        }
    
        public void PlayJumpSfx()
        {
            if (jumpClip is null) return;
            sfxController.PlaySound(jumpClip);
        }
    
        public void PlayMovementSfx()
        {
            if (movementClip is null) return;
            sfxController.PlaySound(movementClip);
        }

        public void PlayLandingSfx(bool isGrounded, float velocity)
        {
            if (!isGrounded) return;
            if (landingClips.Count <= 0) return;
        
            float level = landingLevels.Evaluate(velocity);
            int index = Mathf.FloorToInt(level * landingClips.Count);
            sfxController.PlaySound(landingClips[index]);
        }
    
        private void OnDestroy()
        {
            playerController.Jumped -= PlayJumpSfx;
            playerController.Moved -= PlayMovementSfx;
            playerController.GroundedChanged -= PlayLandingSfx;
        }
    }
}
