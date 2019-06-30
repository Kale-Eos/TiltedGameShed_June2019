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
        Vector3 DesiredPosition = Vector3.zero;
                
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
    }
}
