using System;
using System.Linq;
using Assets.Resources.Scripts.Extensions;
using UnityEngine;

namespace Assets.Resources.Scripts.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class KeyboardMovement3D : MonoBehaviour
    {
        private float speed = 6.0f;
        private float jumpSpeed = 8.0f;
        private float gravity = 20.0f;

        private Vector3 moveDirection = Vector3.zero;

        public void Update()
        {
            var controller = GetComponent<CharacterController>();
            if (controller.isGrounded)
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }

            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }
}