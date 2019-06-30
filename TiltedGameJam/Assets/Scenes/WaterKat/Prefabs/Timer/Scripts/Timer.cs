using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float time;
    float maxTime=60;
    // Start is called before the first frame update
    void Start()
    {
        time = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        time = Mathf.Max(0, time);

        GetComponent<Text>().text = "Time : " + Mathf.Floor(time);
        if (time <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
