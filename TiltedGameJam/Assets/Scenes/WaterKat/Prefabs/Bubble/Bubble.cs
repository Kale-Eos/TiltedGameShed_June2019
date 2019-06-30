using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat {
    public class Bubble : MonoBehaviour, IEntity
    {
        int health = 1;
        int maxHealth = 1;
        [SerializeField]
        private float speed;
        [SerializeField]
        private float bounce = 20;
        public Vector3 DesiredPosition = Vector3.zero;
                
        public int Health { get { return health; } set { } }

        public void Damage(int _damage)
        {
            health = 0;
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public void Heal(int _heal)
        {
            //throw new System.NotImplementedException();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector3 difference = DesiredPosition - transform.position;
            if (difference.magnitude > 0.5)
            {
                GetComponent<Rigidbody>().velocity = difference.normalized * speed * Mathf.Pow(difference.magnitude,2);
            }
            else
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
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
