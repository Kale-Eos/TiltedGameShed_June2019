using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace WaterKat
{
    public class Scr_Camera : MonoBehaviour
    {
        public DummyCamera PlayerCamera;
        Transform PlayerTransform;
        Rigidbody PlayerRigidBody;

        Vector3 GeneralOffset = new Vector3(0, 1f, 0);

        public float CameraZoomSpeed = 0.05f;

        Vector3 GunPivot = new Vector3(0f, 0f, 0f);
        Vector3 GunOffset = new Vector3(0f, 0f, -2.5f);

        public Vector2 CameraXClamp;
        //public Vector2 CameraYClamp;

        [SerializeField]
        float CameraXRotation = 0f;
        [SerializeField]
        float CameraYRotation = 0f;

        Vector3 PlatformerPivot = new Vector3(0f, 10, 0f);
        Vector3 PlatformerOffset = new Vector3(0f, 0, -19f);


        public int CameraMode = 0;

        private float inputTransition = 0;
        [Range(0, 1)]
        public float InputTransition = 0;

        public float MouseSensitivity = 2;

        public float CameraZDistance = 1;

        void Start()
        {
            //PlayerCamera = GetComponentInChildren<DummyCamera>();
            PlayerTransform = transform.Find("HitBox");
            PlayerRigidBody = PlayerTransform.gameObject.GetComponent<Rigidbody>();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public bool PLAYERLOCK = true;
        private void Update()
        {
            if (PLAYERLOCK)
            {
                if (Input.GetKey("escape"))
                {
                    UnityEditor.EditorApplication.isPlaying = false;
                }
                if (Input.GetKey("`"))
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                if (Input.GetMouseButton(0))
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;

                }
            }
        }

        void AdjustPlayer()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {

                inputTransition = Mathf.Clamp(inputTransition + -Input.mouseScrollDelta.y*CameraZoomSpeed, 0, 1);


            CameraXRotation += -WKInput.instance.CameraY.Get() * MouseSensitivity * Mathf.Clamp(1f, 1.25f, CameraXClamp.y);
            CameraYRotation += WKInput.instance.CameraX.Get() * MouseSensitivity * Mathf.Clamp(1f, 1.25f, CameraXClamp.y);

            /*
            CameraZDistance = Mathf.Min(CameraXRotation, 20);
            if (CameraZDistance < 20)
            {
                CameraZDistance = Mathf.Pow(1 - Mathf.Abs(CameraXRotation) / 90f, 2);
            }
            else
            {
                CameraZDistance = 1;
            }*/

            CameraXRotation = Mathf.Clamp(CameraXRotation, CameraXClamp.x, CameraXClamp.y);
            //CameraYRotation = Mathf.Clamp(CameraYRotation, CameraYClamp.x, CameraYClamp.y);


            //GetComponent<Movement>().speed = Mathf.Lerp(GetComponent<Movement>().speed/2, GetComponent<Movement>().speed , inputTransition);

            InputTransition = Mathf.Pow(inputTransition, 3) * (inputTransition * (6f * inputTransition - 15f) + 10f);

            Quaternion CameraRotation = Quaternion.Euler(CameraXRotation, CameraYRotation, 0);

            Vector3 GunPosition = PlayerTransform.position + GeneralOffset + ((CameraRotation * GunOffset) * CameraZDistance);

            Vector3 PlatformerPosition = PlayerTransform.position + GeneralOffset + ((CameraRotation * PlatformerOffset) * CameraZDistance);
            Quaternion PlatformerRotation = CameraRotation;

            var spawnPoint= Vector3.Lerp(GunPosition, PlatformerPosition, InputTransition);

            var hitColliders = Physics.OverlapSphere(spawnPoint, 0.5f);//1 is purely chosen arbitrarly


                PlayerCamera.transform.rotation = Quaternion.Lerp(CameraRotation, PlatformerRotation, InputTransition);
                PlayerCamera.transform.position = Vector3.Lerp(GunPosition, PlatformerPosition, InputTransition);

            /*
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            ray.origin = Camera.main.transform.position + (Camera.main.transform.forward * Vector3.Lerp(GunPosition, PlatformerPosition, InputTransition).z * -1);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hit.point = Camera.main.transform.position + (ray.direction * 100);
            }
            if (InputTransition < 1)
            {
                Vector3 tempvel = GetComponentInChildren<Rigidbody>().velocity;
                tempvel.y = 0;
                //transform.Find("HitBox").transform.LookAt(transform.Find("HitBox").transform.position + tempvel);
                //transform.Find("HitBox").transform.Find("Gun").transform.LookAt(hit.point);
            }
            else
            {
                if (GetComponent<Rigidbody>().velocity.magnitude < 0.01f)
                {
                    //transform.Find("HitBox").transform.LookAt(transform.Find("HitBox").transform.position + (Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * Vector3.forward * 10));
                    //transform.Find("HitBox").transform.Find("Gun").transform.rotation = new Quaternion();
                }
                else
                {
                    Vector3 tempvel = GetComponentInChildren<Rigidbody>().velocity;
                    tempvel.y = 0;
                    transform.Find("HitBox").transform.LookAt(transform.Find("HitBox").transform.position + tempvel);
                    //transform.Find("HitBox").transform.Find("Gun").transform.rotation = new Quaternion();
                }
            }
            */
        }
    }
}