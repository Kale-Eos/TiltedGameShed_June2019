using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WKInput : MonoBehaviour
{
    public static WKInput instance;

    [System.Serializable]
    public struct InputKey
    {
        public List<string> ButtonIDs;
        public List<string> KeyIDs;

        public bool Down()
        {
            bool down = false;
            foreach (string button in ButtonIDs)
            {
                if (Input.GetButtonDown(button))
                {
                    down = true;
                }
            }
            foreach (string key in KeyIDs)
            {
                if (Input.GetKeyDown(key))
                {
                    down = true;
                }
            }
            return down;
        }

        public bool Held()
        {
            bool held = false;
            foreach (string button in ButtonIDs)
            {
                if (Input.GetButton(button))
                {
                    held = true;
                }
            }
            foreach (string key in KeyIDs)
            {
                if (Input.GetKey(key))
                {
                    held = true;
                }
            }
            return held;
        }

        public bool Up()
        {
            bool up = false;
            foreach (string button in ButtonIDs)
            {
                if (Input.GetButtonUp(button))
                {
                    up = true;
                }
            }
            foreach (string key in KeyIDs)
            {
                if (Input.GetKeyUp(key))
                {
                    up = true;
                }
            }
            return up;
        }
    }
    [System.Serializable]
    public struct InputAxis
    {
        [System.Serializable]
        public struct InputKeyPair
        {
            public string Negative;
            public string Positive;
        }

        public List<string> AxisIDs;
        public List<InputKeyPair> KeyIDs;

        public float Get()
        {
            float finalaxis = 0f;
            List<float> axislist = new List<float>();

            foreach (string axis in AxisIDs)
            {
                axislist.Add(Input.GetAxis(axis));
            }
            foreach (InputKeyPair keypair in KeyIDs)
            {
                float keyaxis = 0;
                if (Input.GetKey(keypair.Negative))
                {
                    keyaxis += -1f;
                }
                if (Input.GetKey(keypair.Positive))
                {
                    keyaxis += 1f;
                }
                axislist.Add(keyaxis);
            }

            foreach  (float i in axislist)
            {
                finalaxis += i;
            }

            //finalaxis = finalaxis / axislist.Count;
            //finalaxis = Mathf.Clamp(finalaxis, -1f, 1f);

            return finalaxis;
        }
    }

    [SerializeField]
    public InputKey Jump;

    public InputAxis MovementX;
    public InputAxis MovementY;

    public InputAxis CameraX;
    public InputAxis CameraY;

    private void Awake()
    {
        instance = this;
    }
}
