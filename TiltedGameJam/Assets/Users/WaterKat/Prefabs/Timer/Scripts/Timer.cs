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
        [SerializeField]
        float maxTime = 10;
        public Vector3 rectTransform;
        // Start is called before the first frame update
        void Start()
        {
            rectTransform = this.gameObject.GetComponent<RectTransform>().localScale;
            time = maxTime;
        }

        bool[] signed = new bool[5] { false, false, false, false, false };
        string[] Alarms = new string[5] { "One","Two","Three","Four","Five"};

        // Update is called once per frame
        void Update()
        {
            time -= Time.deltaTime;
            time = Mathf.Max(0, time);

            GetComponent<Text>().text = "Time " + Mathf.Floor(time/60)+":" + Mathf.Floor(time%60).ToString("00");

            for (int i = 0; i < 5; i++)
            {
                if (((int)Mathf.Floor(time) == i + 1) && (!signed[i]))
                {
                    TimerManager.AddLoopedTask(Throb, 1);
                    signed[i] = true;
                    WKAudio.PlayAudio("" + Alarms[i]);
                }
            }
            

            if (time <= 0)
            {
                TimerManager.AddLoopedTask(Throb, 1);
                WKAudio.PlayAudio("Death");
                AudioManager.instance.StopSound("Music");
                WKAudio.PlayAudio("GameOver");
                SceneManager.LoadScene("GameOver");
            }
        }
        void Throb(float testvalue)
        {
            this.gameObject.GetComponent<RectTransform>().localScale = rectTransform * Mathf.Lerp(.75f,1.05f,testvalue);
        }
    }
}
