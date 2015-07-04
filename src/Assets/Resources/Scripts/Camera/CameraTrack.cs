using UnityEngine;

namespace Assets.Resources.Scripts.Camera
{
    public class CameraTrack : MonoBehaviour
    {
        public GameObject GameObject;
        public Vector3 Offset;
        public float Leeway;

        void Start()
        {
            transform.position = GameObject.transform.position + Offset;
        }

        void Update()
        {
            transform.position = GameObject.transform.position + Offset;
        }
    }
}