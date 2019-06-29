using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat
{
    public class Player : MonoBehaviour
    {
        #region "Grounded"
        public float GroundDistance = 0.5f;
        float SphereRadius =0f;

        public bool CheckIfGrounded()
        {
            bool Grounded = false;
            Ray downwards = new Ray(transform.position, Vector3.down * (1 - SphereRadius + GroundDistance));
            RaycastHit hit;

            if (Physics.Raycast(downwards,  out hit, downwards.direction.magnitude))
            {
                Grounded = true;
            }
            return Grounded;
        }
        public bool CheckIfGrounded(out Vector3 _groundVelocity)
        {
            bool Grounded = false;
            Ray downwards = new Ray(transform.position, Vector3.down * (1-SphereRadius + GroundDistance));
            RaycastHit hit;
            if (Physics.SphereCast(downwards,SphereRadius, out hit, downwards.direction.magnitude))
            {
                Grounded = true;
                Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    _groundVelocity = rb.velocity;
                }
            }
            _groundVelocity = Vector3.zero;
            return Grounded;
        }
        #endregion


    }
}