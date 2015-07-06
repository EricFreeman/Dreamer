using System.Collections.Generic;
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

        private Rigidbody2D _rigidbody2D;

        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            var horizontalMovement = Input.GetAxisRaw("Horizontal") * MaxMovementSpeed;

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                Jump();
            }

            _rigidbody2D.velocity = new Vector2(horizontalMovement, _rigidbody2D.velocity.y.Clamp(-MaxGravity, MaxGravity));
        }

        private bool IsGrounded()
        {
            return GetRaycastHits().Any();
        }

        private IEnumerable<RaycastHit2D> GetRaycastHits()
        {
            // Check if right side of player or left side of player is touching.
            // This will account for being slightly over a ledge or something.
            RaycastHit2D[] hits =
            {
                Physics2D.Raycast(transform.position + new Vector3(-.12f, -.4f), -Vector2.up, .05f),
                Physics2D.Raycast(transform.position + new Vector3(0, .4f), -Vector2.up, .05f),
                Physics2D.Raycast(transform.position + new Vector3(.12f, -.4f), -Vector2.up, .05f)
            };
            hits.Where(x => x.collider != null).ToList().ForEach(x => Debug.Log(x.collider.name));
            return hits.Where(x => x.collider != null && x.collider.tag != "Player");
        }

        private void Jump()
        {
            transform.SetParent(null);
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, JumpForce);
            _rigidbody2D.gravityScale = 1;
        }
    }
}