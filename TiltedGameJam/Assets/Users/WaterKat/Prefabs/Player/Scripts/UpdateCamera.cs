using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UpdateCamera : MonoBehaviour
{
    public DummyCamera dummyCamera;
       public Camera camera;
    public GameObject player;
    List<Vector3> AveragePositionList = new List<Vector3>();
    List<Vector3> AverageLookList = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        //dummyCamera = GetComponentInChildren<DummyCamera>();
        //camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 difference = (dummyCamera.transform.position - camera.transform.position);
        if (difference.magnitude > 0.5f)
        {
            camera.GetComponent<Rigidbody>().velocity = difference.normalized * Mathf.Min((Mathf.Pow(difference.magnitude, 3) + 2),1000);
        }
        else
        {
            camera.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        camera.transform.LookAt(player.transform.position);
    }
}
