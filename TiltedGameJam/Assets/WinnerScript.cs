using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WaterKat;

public class WinnerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
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
                testplayer.Win();

            }
        }
        WKAudio.PlayAudio("Win");
    }
}
