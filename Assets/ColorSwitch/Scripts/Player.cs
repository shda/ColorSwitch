using System;
using ColorSwitch.Scripts.Obstacles;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float jumpForce = 10f;
        
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private SpriteRenderer bodyRenderer;
        [SerializeField] private ColorsStorage colorsStorage;

        public Game Game { get; set; }

        public Action OnGameOverEvent;
        public ColorInGame CurrentColor { get; private set; }
        private void Awake()
        {
            SetColor(ColorInGame.Cyan);
            inputHandler.OnTap = OnTap;
        }

        private void OnTap()
        {
            body.linearVelocity = Vector2.up * jumpForce;
        }

        public void SetColor(ColorInGame color)
        {
            CurrentColor = color;
            bodyRenderer.color = colorsStorage.GetColor(color);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var cross = other.GetComponent<CrossedTrigger>();
            cross?.OnPlayerTriggerEnter(this , Game);
        }

        public void OnGameOver()
        {
            OnGameOverEvent?.Invoke();
        }

        public void Stop()
        {
            gameObject.SetActive(false);
        }
    }
}