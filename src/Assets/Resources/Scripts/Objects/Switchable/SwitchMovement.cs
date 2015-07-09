using UnityEngine;
using Assets.Resources.Scripts.Extensions;

namespace Assets.Resources.Scripts.Objects.Switchable
{
    public class SwitchMovement : MonoBehaviour
    {
        private bool _isOn;
        private Vector3 _initial;

        public Vector3 EndPosition;
        public float Speed = 1f;

        void Start()
        {
            _initial = transform.localPosition;
        }

        void Update()
        {
            transform.localPosition = transform.localPosition.MoveTowards(_isOn ? EndPosition : _initial, Speed * Time.deltaTime);
        }

        public void Switch()
        {
            _isOn = !_isOn;
        }
    }
}