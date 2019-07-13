using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat;

public class LavaDeath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Player testplayer = collision.collider.gameObject.GetComponent<Player>();
        if (testplayer != null)
        {
            Rigidbody rb = collision.collider.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                testplayer.Die();

            }
        }
        WKAudio.PlayAudio("Die");
    }
}
