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

        public Rigidbody2D RigidBody;

        void Start()
        {
            RigidBody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            var horizontalMovement = Input.GetAxisRaw("Horizontal") * MaxMovementSpeed;

            RigidBody.velocity = new Vector2(horizontalMovement, RigidBody.velocity.y.Clamp(-MaxGravity, MaxGravity));
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
                Physics2D.Raycast(transform.position - new Vector3(-.12f, .18f), -Vector2.up, .05f),
                Physics2D.Raycast(transform.position - new Vector3(0, .18f), -Vector2.up, .05f),
                Physics2D.Raycast(transform.position - new Vector3(.12f, .18f), -Vector2.up, .05f)
            };
            return hits.Where(x => x.collider != null && x.collider.tag != "Player");
        }
    }
}