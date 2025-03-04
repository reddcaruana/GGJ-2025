using UnityEngine;

namespace WitchesBasement.System
{
    public class SpinAnimation : MonoBehaviour
    {
        [SerializeField] private float interval = 0.25f;
        
        private float time;

#region Lifecycle Events

        private void Start()
        {
            time = Random.value;
        }

        private void Update()
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                return;
            }
            
            var eulerAngles = transform.eulerAngles;
            eulerAngles.z = (eulerAngles.z - 45) % 360;
            transform.eulerAngles = eulerAngles;

            time = interval;
        }

#endregion
    }
}