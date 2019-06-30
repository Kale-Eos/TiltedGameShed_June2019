using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
namespace WaterKat {
    public class Timer : MonoBehaviour
    {
        [SerializeField]
        float time;
        float maxTime = 10;
        // Start is called before the first frame update
        void Start()
        {
            time = maxTime;
        }

        bool[] signed = new bool[5] { false, false, false, false, false };
        string[] Alarms = new string[5] { "One","Two","Three","Four","Five"};

        // Update is called once per frame
        void Update()
        {
            time -= Time.deltaTime;
            time = Mathf.Max(0, time);

            GetComponent<Text>().text = "Time : " + Mathf.Floor(time);

            for (int i = 0; i < 5; i++)
            {
                if (((int)Mathf.Floor(time) == i + 1) && (!signed[i]))
                {
                    signed[i] = true;
                    WKAudio.PlayAudio("Alarm" + Alarms[i]);
                }
            }
            

            if (time <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
