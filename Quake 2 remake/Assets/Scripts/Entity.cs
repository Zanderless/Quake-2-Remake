using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q2R
{
    public class Entity : MonoBehaviour
    {

        private float _health;
        public float maxHealth;
        public float Health
        {
            get { return _health; }
            set
            {
                _health = value;
                _health = Mathf.Clamp(_health, 0, maxHealth);

                if (_health == 0)
                    Die();
            }
        }

        private float _armor;
        public float maxArmor;
        public float Armor
        {
            get { return _armor; }
            set
            {
                _armor = value;
                _armor = Mathf.Clamp(_armor, 0, maxArmor);
            }
        }

        public Vector3 EntityPos => transform.position;
        public Quaternion EntityRot => transform.rotation;

        public virtual void Die()
        {
            throw new NotImplementedException();
        }
    }
}
