﻿using System;
using System.Linq;
using Assets.Resources.Scripts.Extensions;
using UnityEngine;

namespace Assets.Resources.Scripts.Player
{
    public class KeyboardMovement : MonoBehaviour
    {
        public float MaxMovementSpeed;
        public float MaxGravity;
        public float JumpForce;

        public GameObject DustParticle;

        private bool _hasDoubleJumped;
        private float _currentJumpForce;
        private Rigidbody2D _rigidbody2D;

        public float JumpDistance;

        public float DustSpawnDelay;
        private float _timeSinceLastDustSpawn;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            var horizontalMovement = Input.GetAxisRaw("Horizontal") * MaxMovementSpeed;
            if(Math.Abs(_currentJumpForce) > .1f)
            {
                horizontalMovement += _currentJumpForce;
                _currentJumpForce = _currentJumpForce.MoveTowards(0, .3f);
            }

            var cachedIsGrounded = IsGrounded();

            if (cachedIsGrounded)
            {
                _hasDoubleJumped = false;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GetRightHits())
                {
                    Jump(-JumpForce * .25f);
                }

                if (GetLeftHits())
                {
                    Jump(JumpForce * .25f);
                }

                if (cachedIsGrounded)
                {
                    Jump();
                }
            }

            _rigidbody2D.velocity = new Vector2(horizontalMovement, _rigidbody2D.velocity.y.Clamp(-MaxGravity, MaxGravity));

            _timeSinceLastDustSpawn += Time.deltaTime;
            if (cachedIsGrounded && _timeSinceLastDustSpawn > DustSpawnDelay && Math.Abs(horizontalMovement) > 0)
            {
                _timeSinceLastDustSpawn = 0;
                SpawnDust();
            }
        }

        private bool IsGrounded()
        {
            RaycastHit2D[] hits =
            {
                Physics2D.Raycast(transform.position + new Vector3(-.12f, -.4f), -Vector2.up, JumpDistance),
                Physics2D.Raycast(transform.position + new Vector3(0, .4f), -Vector2.up, JumpDistance),
                Physics2D.Raycast(transform.position + new Vector3(.12f, -.4f), -Vector2.up, JumpDistance)
            };

            return hits.Where(x => x.collider != null && x.collider.tag != "Player").Any();
        }

        private bool GetLeftHits()
        {
            RaycastHit2D[] hits =
            {
                Physics2D.Raycast(transform.position + new Vector3(-.4f, -.12f), Vector2.left, JumpDistance),
                Physics2D.Raycast(transform.position + new Vector3(-.4f, 0), Vector2.left, JumpDistance),
                Physics2D.Raycast(transform.position + new Vector3(-.4f, -.12f), Vector2.left, JumpDistance)
            };

            return hits.Where(x => x.collider != null && x.collider.tag != "Player").Any();
        }

        private bool GetRightHits()
        {
            RaycastHit2D[] hits =
            {
                Physics2D.Raycast(transform.position + new Vector3(.4f, -.12f), Vector2.right, JumpDistance),
                Physics2D.Raycast(transform.position + new Vector3(.4f, 0), Vector2.right, JumpDistance),
                Physics2D.Raycast(transform.position + new Vector3(.4f, -.12f), Vector2.right, JumpDistance)
            };

            return hits.Where(x => x.collider != null && x.collider.tag != "Player").Any();
        }

        private void Jump(float? horizontal = null)
        {
            _currentJumpForce = horizontal ?? 0;
            transform.SetParent(null);
            _rigidbody2D.velocity = new Vector2(horizontal ?? _rigidbody2D.velocity.x, JumpForce);
            _rigidbody2D.gravityScale = 1;
        }

        private void SpawnDust()
        {
            var dust = Instantiate(DustParticle);
            dust.transform.position = transform.position;
        }
    }
}