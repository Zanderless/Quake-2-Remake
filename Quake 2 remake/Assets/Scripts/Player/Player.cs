using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Q2R
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class Player : Entity
    {

        private float ForwardSpeed => 10f;
        private float StraifSpeed => 7f;
        private float JumpForce => 1.25f;

        private Vector3 velocity;
        private Rigidbody Controller => GetComponent<Rigidbody>();
        private CapsuleCollider Col => GetComponent<CapsuleCollider>();

        private bool IsGrounded => Grounded();

        private void Start()
        {
            Health = maxHealth;
            Armor = 0;
        }

        private void Update()
        {

            Move();

        }

        private void FixedUpdate()
        {
            Controller.MovePosition(Controller.position + velocity * Time.deltaTime);
        }

        private void Move()
        {

            float v = Input.GetAxis("Vertical") * ForwardSpeed;
            float h = Input.GetAxis("Horizontal") * StraifSpeed;

            velocity = new Vector3(h, 0, v);

            velocity = transform.TransformDirection(velocity);

            if (IsGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    Controller.AddForce(Vector3.up * Mathf.Sqrt(JumpForce * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            }

        }

        private bool Grounded()
        {

            float halfHeight = Col.height / 2;

            Vector3 bottomPos = new Vector3(transform.position.x, transform.position.y - halfHeight, transform.position.z);

            Collider[] cols = Physics.OverlapBox(bottomPos, new Vector3(0.1f, 0.2f, 0.1f));

            return cols.Length > 1;

        }

        public override void Die()
        {
            print("Dead");
        }
    }
}
