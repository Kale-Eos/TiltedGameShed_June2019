using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WaterKat
{
    public interface IEntity
    {
        int Health { get; set; }
        void Heal(int _heal);
        void Damage(int _damage);
        void Die();
    }

    public class WKAudio
    {
        public static void PlayAudio(string name)
        {
            AudioManager manager = MonoBehaviour.FindObjectOfType<AudioManager>();   
            if (manager != null)
            {
                manager.PlaySound(name);
            }
        }
    }
}