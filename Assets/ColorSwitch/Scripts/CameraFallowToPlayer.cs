using UnityEngine;

namespace ColorSwitch.Scripts
{
    [ExecuteInEditMode]
    public class CameraFallowToPlayer : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Player player;
        [SerializeField] private float mod = 3.0f;
        
        private Vector3 targetPosition;
        
        private void Update()
        {
            var position = player.transform.position;
            if (position.y > cameraTransform.position.y)
            {
                targetPosition = player.transform.position;
            }
            
            cameraTransform.transform.position = Vector3.Lerp(
                cameraTransform.transform.position  , targetPosition ,Time.deltaTime * mod);
        }
    }
}