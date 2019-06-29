using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WKOld
{
    public class Movement : MonoBehaviour
    {
        Player CurrentPlayer;

        GameForce PlayerGameForce = GameForce.Zero;
        List<GameForce> LocalForceList = new List<GameForce>();

        //  float CharacterAngle;
        bool IsGrounded = false;
        public float speed;

        private class GameForce
        {
            public Vector3 Direction;
            public float AccelerationTime;
            public float AntiMult;
            public Vector3 MaxVelocity;

            public static GameForce Zero
            {
                get
                {
                    GameForce TempGF = new GameForce
                    {
                        Direction = Vector3.zero,
                        AccelerationTime = 1f,
                        AntiMult = 1f,
                        MaxVelocity = Vector3.one
                    };
                    return TempGF;
                }
            }
        }

        void Update()
        {
            bool Grounded = CheckIfGrounded();
            test();

        }



        private bool CheckIfGrounded()
        {
            Vector3 thing;
            IsGrounded = CurrentPlayer.CheckIfGrounded(out thing);
            return IsGrounded;
        }

        private void Start()
        {
            CurrentPlayer = GetComponent<Player>();
        }

        public PhysicMaterial Move;
        public PhysicMaterial Stay;

        void test()
        {
            Vector3 Direction = new Vector3(WKInput.instance.MovementX.Get(), 0, -WKInput.instance.MovementY.Get());
            Vector3 DirectionalVelocity = (Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * Direction * speed);


            if (!CurrentPlayer.CheckIfGrounded())
            {
                GetComponent<Collider>().material = Move;
            }
            else
            {
                if (Direction.magnitude < 0.1f)
                {
                    GetComponent<Collider>().material = Stay;
                }
                else
                {
                    GetComponent<Collider>().material = Move;
                }
            }
            //Debug.Log("Moving");
            transform.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(DirectionalVelocity.x, transform.gameObject.GetComponent<Rigidbody>().velocity.y, DirectionalVelocity.z);
        }
    }
}