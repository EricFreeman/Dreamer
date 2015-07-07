using Assets.Resources.Scripts.Extensions;
using UnityEngine;

namespace Assets.Resources.Scripts.Environment
{
    public class Water : MonoBehaviour
    {
        public float UpwardForce;
        public float MaxUpwardForce;

        private bool _isUnderwater;
        private Rigidbody2D _rigidbody2D;

        private float _startDrag;

        void Start()
        {
            _rigidbody2D = GameObject.Find("Player").GetComponent<Rigidbody2D>();
            _startDrag = _rigidbody2D.drag;
        }

        void Update()
        {
            if (_isUnderwater)
            {
                var currentVelocity = _rigidbody2D.velocity;
                _rigidbody2D.velocity = new Vector2(currentVelocity.x, currentVelocity.y.Clamp(-MaxUpwardForce, MaxUpwardForce));
                _rigidbody2D.AddForce(new Vector2(0, UpwardForce));
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            var col = collision.transform;
            if (col.name == "Player")
            {
                _isUnderwater = true;
                _rigidbody2D.drag = 1f;
            }
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            var col = collision.transform;
            if (col.name == "Player")
            {
                _isUnderwater = false;
                _rigidbody2D.drag = _startDrag;
            }
        }
    }
}