using System;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    public class PlayerMoving : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float maxJumpDistance = 2f;
        
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private Rigidbody2D body;

        private float _maxPlayerYPosition;
        
        private void Awake()
        {
            inputHandler.OnTap = OnTap;
        }
        
        private void FixedUpdate()
        {
            if (transform.position.y > _maxPlayerYPosition + maxJumpDistance)
            {
                body.linearVelocity =  Vector2.zero;
            }
        }

        private void OnTap()
        {
            _maxPlayerYPosition = transform.position.y + maxJumpDistance;
            body.linearVelocity = Vector2.up * jumpForce;
        }
    }
}