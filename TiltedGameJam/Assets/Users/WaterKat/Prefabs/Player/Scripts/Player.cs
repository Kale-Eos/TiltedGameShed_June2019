using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WaterKat
{
    public class Player : MonoBehaviour
    {
        #region "Grounded"
        public float GroundDistance = 2f;
        float SphereRadius =0f;
        public Transform gameObject;
        public bool CheckIfGrounded()
        {
            bool Grounded = false;
            Ray downwards = new Ray(gameObject.position, Vector3.down * GroundDistance);
            RaycastHit hit;

            if (Physics.Raycast(downwards,  out hit, GroundDistance*2))
            {
                Grounded = true;
            }
            return Grounded;
        }



        public bool CheckIfGrounded(out Vector3 _groundVelocity)
        {
            bool Grounded = false;
            Ray downwards = new Ray(gameObject.position, Vector3.down * (1-SphereRadius + GroundDistance));
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

        internal void Die()
        {
            WKAudio.PlayAudio("Death");
            AudioManager.instance.StopSound("Music");
            WKAudio.PlayAudio("GameOver");
            SceneManager.LoadScene("GameOver");
        }
        internal void Win()
        {

            AudioManager.instance.StopSound("Music");
            WKAudio.PlayAudio("Win");
            SceneManager.LoadScene("Winner");
        }

    }
}