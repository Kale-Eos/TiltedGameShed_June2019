using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat {
    public class StaticBubble : MonoBehaviour
    {
        [SerializeField]
        private float bounce = 20;

        public void Die()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player testplayer = collision.collider.gameObject.GetComponent<Player>();
            if (testplayer != null)
            {
                Rigidbody rb = collision.collider.gameObject.GetComponent<Rigidbody>();
                if (rb!= null)
                {
                    rb.velocity = rb.velocity + (Vector3.up * bounce);

                }
                Jump jump = collision.collider.gameObject.GetComponent<Jump>();
                if (jump != null)
                {
                    jump.JumpState = Jump.PlayerJumpState.SlowFalling;

                }
            }
            WKAudio.PlayAudio("Pop");
            Destroy(this.gameObject);
        }
    }
}
