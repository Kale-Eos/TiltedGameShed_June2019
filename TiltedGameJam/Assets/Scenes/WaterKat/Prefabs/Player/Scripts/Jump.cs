using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WaterKat {
    public class Jump : MonoBehaviour
    {

        public enum PlayerJumpState
        {
            Standing = 0,
            Jumping = 1,
            Rising = 2,
            SlowFalling = 3,
            FreeFalling = 4,
        }

        [SerializeField]
        public PlayerJumpState JumpState = PlayerJumpState.Standing;

        Rigidbody PlayerRB;
        Player CurrentPlayer;


        int AvailableJumps = 2;
        public int MaxAvailableJumps = 2;

        public float GroundJumpHeight;
        public float AirJumpHeight;
        public float GroundJumpTime;

        public float FallingGravityRatio = -1000f;

        float FreefallVelocity;

        float RisingGravity = -1f;
        float FallingGravity = -1f;
        float GroundJumpVelocity = 1000f;
        float AirJumpVelocity = 1000f;

        void Start()
        {
            PlayerRB = transform.GetComponentInChildren<Rigidbody>();
            CurrentPlayer = GetComponent<Player>();

            CalculateDesiredJump();

        }

        void CalculateDesiredJump()
        {
            RisingGravity = Mathf.Max((2f * GroundJumpHeight) / Mathf.Pow(GroundJumpTime, 2),0);
            FallingGravity = RisingGravity * FallingGravityRatio;

            Debug.Log("Gravity" + RisingGravity);

            GroundJumpVelocity = GroundJumpTime * RisingGravity;
            AirJumpVelocity = RisingGravity * Mathf.Sqrt(2 * AirJumpHeight / RisingGravity);

            FreefallVelocity = -GroundJumpVelocity;

            Debug.Log("JumpVelocity" + GroundJumpVelocity);
            Debug.Log("AirJumpVelocity" + AirJumpVelocity);
        }



        void Update()
        {
            Vector3 CurrentVelocity = PlayerRB.velocity;
            Vector3 NewVelocity = CurrentVelocity;

            bool JumpPressed = WKInput.instance.Jump.Down();
            bool JumpHeld = WKInput.instance.Jump.Held();

            Vector3 GroundVelocity=Vector3.zero;
            bool Grounded = CurrentPlayer.CheckIfGrounded();//out GroundVelocity);

            if (Grounded)
            {
                AvailableJumps = MaxAvailableJumps;
            }

            if (((JumpPressed) && (AvailableJumps > 0)) || Input.GetKey("p"))
            {
                AvailableJumps -= 1;
                JumpState = PlayerJumpState.Jumping;

                WKAudio.PlayAudio("Jump");

                if (Grounded)
                {
                    NewVelocity.y = GroundJumpVelocity;
                }
                else
                {
                    NewVelocity.y = AirJumpVelocity;
                }
            }

            switch (JumpState)
            {
                case PlayerJumpState.Standing:
                    if (Grounded)
                    {
                        NewVelocity.y += -1f * Time.deltaTime;
                    }
                    else
                    {
                        JumpState = PlayerJumpState.SlowFalling;
                        goto case PlayerJumpState.SlowFalling;
                    }
                    break;

                case PlayerJumpState.Jumping:
                    if (!JumpHeld)
                    {
                        JumpState = PlayerJumpState.FreeFalling;
                        goto case PlayerJumpState.FreeFalling;
                    }
                    if (CurrentVelocity.y < 0)
                    {
                        JumpState = PlayerJumpState.SlowFalling;
                        goto case PlayerJumpState.SlowFalling;
                    }
                    goto case PlayerJumpState.SlowFalling;
                    //break;

                case PlayerJumpState.SlowFalling:
                    if (!JumpHeld)
                    {
                        JumpState = PlayerJumpState.FreeFalling;
                        goto case PlayerJumpState.FreeFalling;
                    }
                    if (CurrentVelocity.y < FreefallVelocity)
                    {
                        JumpState = PlayerJumpState.FreeFalling;
                        goto case PlayerJumpState.FreeFalling;
                    }
                    if ((Grounded) && (CurrentVelocity.y - GroundVelocity.y) < 0)
                    {
                        JumpState = PlayerJumpState.Standing;
                        goto case PlayerJumpState.Standing;
                    }
                    NewVelocity.y += -RisingGravity * Time.deltaTime;
                    break;

                case PlayerJumpState.FreeFalling:
                    if ((Grounded) && (CurrentVelocity.y - GroundVelocity.y) < 0)
                    {
                        JumpState = PlayerJumpState.Standing;
                        goto case PlayerJumpState.Standing;
                    }
                    NewVelocity.y += -FallingGravity * Time.deltaTime;
                    break;
            }

            PlayerRB.velocity = NewVelocity;
        }
    }
}
