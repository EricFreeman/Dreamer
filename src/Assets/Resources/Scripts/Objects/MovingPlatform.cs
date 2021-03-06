﻿using UnityEngine;

namespace Assets.Resources.Scripts.Objects
{
    public class MovingPlatform : MonoBehaviour
    {
        public Vector3 StartPosition;
        public Vector3 EndPosition;
        public float PlatformSpeed;

        private bool _movingTo;

        void Update()
        {
            if (_movingTo)
            {
                transform.position = Vector3.MoveTowards(transform.position, EndPosition, PlatformSpeed * Time.deltaTime);
                if (transform.position == EndPosition)
                {
                    _movingTo = false;
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, StartPosition, PlatformSpeed * Time.deltaTime);
                if (transform.position == StartPosition)
                {
                    _movingTo = true;
                }
            }
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            var col = collision.collider.transform;
            if (col.name == "Player")
            {
                col.SetParent(transform);
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            var col = collision.collider.transform;
            if (col.name == "Player")
            {
                col.SetParent(null);
            }
        }
    }
}