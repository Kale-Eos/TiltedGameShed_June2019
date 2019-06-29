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

    }
}