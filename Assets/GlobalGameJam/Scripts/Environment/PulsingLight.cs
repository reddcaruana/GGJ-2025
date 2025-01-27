using UnityEngine;

namespace GlobalGameJam.Environment
{
    [RequireComponent(typeof(Light))]
    public class PulsingLight : MonoBehaviour
    {
        [SerializeField] private float minIntensity = 1.0f;
        [SerializeField] private float maxIntensity = 5.0f;
        [SerializeField] private float duration = 3.0f;

        private Light attachedLight;

        private float value;
        private int direction = 1;

#region Lifecycle Events

        private void Awake()
        {
            attachedLight = GetComponent<Light>();
        }

        private void Update()
        {
            value += Time.deltaTime;
            if (value >= duration)
            {
                value = 0.0f;
                direction *= -1;
            }

            var t = value / duration;
            attachedLight.intensity = direction > 0
                ? Mathf.Lerp(minIntensity, maxIntensity, t)
                : Mathf.Lerp(minIntensity, maxIntensity, 1 - t);
        }

#endregion
    }
}