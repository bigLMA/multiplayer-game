using Misc.Sound;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Audio;

namespace Components.Moving
{
    public class MovingComponent : NetworkBehaviour
    {
        [Header("Moving")]
        [SerializeField]
        [Tooltip("Move speed")]
        [Range(1f, 15f)]
        private float speed = 6.5f;

        [Header("Moving")]
        [SerializeField]
        [Tooltip("Turn speed")]
        [Range(8f, 45f)]
        private float turnSpeed = 20f;

        [SerializeField]
        [Tooltip("Sounds player make on movement")]
        private List<AudioResource> sounds;

        /// <summary>
        /// Current player direction (zero vector if player not moving)
        /// </summary>
        public Vector2 direction { get; set; } = Vector2.zero;

        /// <summary>
        /// Animator component
        /// </summary>
        private Animator animator;

        /// <summary>
        /// Walking particles
        /// </summary>
        private ParticleSystem walkParticles;

        /// <summary>
        /// Play sound
        /// </summary>
        private IPlaySound playSound;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            animator = GetComponent<Animator>();
            walkParticles = GetComponent<ParticleSystem>();

            playSound = new PlayRandomSound(GetComponent<AudioSource>(), sounds);
        }

        void FixedUpdate()
        {
           if(IsServer && IsLocalPlayer)
            {
                Move();
            }
           else if(IsLocalPlayer)
            {
                MoveServerRpc();
            }
        }

        [ServerRpc]
        private void MoveServerRpc()
        {
            MoveClientRpc();
        }

        [ClientRpc]
        private void MoveClientRpc()
        {
            Move();
        }

        private void Move()
        {
            // If direction is set by player input
            if (direction != Vector2.zero)
            {
                var desiredForward = new Vector3(direction.x, transform.forward.y, direction.y);

                // Turn object toward new forward
                transform.forward = Vector3.Slerp(transform.forward, desiredForward, turnSpeed * Time.fixedDeltaTime);

                // If object is not facing obstacle
                if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, transform.forward + Vector3.up * 0.5f, 0.5f))
                {
                    // Translate object forward
                    transform.Translate(0f, 0f, Time.fixedDeltaTime * speed);

                    // Play walking animation
                    animator.SetBool("IsWalking", true);

                    PlayParticlesServerRpc();
                }
            }
            else // Otherwise
            {
                // Play idle animation
                animator.SetBool("IsWalking", false);

                StopParticlesServerRpc();
            }
        }

        [ServerRpc]
        private void StopParticlesServerRpc()
        {
            StopParticlesClientRpc();
        }

        [ClientRpc]
        private void StopParticlesClientRpc()
        {
            // Stop particles
            walkParticles.Stop();
        }

        [ServerRpc]
        private void PlayParticlesServerRpc()
        {
            PlayParticlesClientRpc();
        }

        [ClientRpc]
        private void PlayParticlesClientRpc()
        {
            // Play particles
            walkParticles.Play();
        }

        public void PlayWalkSound()=>playSound.Play();
    }
}
