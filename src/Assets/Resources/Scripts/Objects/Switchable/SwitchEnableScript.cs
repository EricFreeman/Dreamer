using UnityEngine;

namespace Assets.Resources.Scripts.Objects.Switchable
{
    public class SwitchableObject : MonoBehaviour
    {
        public bool IsOn;
        public string ScriptToEnable;

        private MonoBehaviour _script;

        public void Start()
        {
            _script = (MonoBehaviour)GetComponent(ScriptToEnable);
        }

        public void Switch()
        {
            IsOn = !IsOn;
            _script.enabled = IsOn;
        }
    }
}