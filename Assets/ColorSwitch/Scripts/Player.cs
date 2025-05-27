using System;
using ColorSwitch.Scripts.Obstacles;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer bodyRenderer;
        [SerializeField] private SpriteCollectionByColor spriteCollectionByColor;

        public Game Game { get; set; }

        public Action OnGameOverEvent;
        public ColorInGame CurrentColor { get; private set; }
        private void Awake()
        {
            SetColor(ColorInGame.Blue);
            
        }
        
        public void SetColor(ColorInGame color)
        {
            CurrentColor = color;
            bodyRenderer.sprite = spriteCollectionByColor.GetSpriteByColor(color);
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