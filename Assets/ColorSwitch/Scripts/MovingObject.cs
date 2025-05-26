using ColorSwitch.Scripts.Obstacles;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] private SpriteCollectionByColor spriteCollectionByColor;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ObstacleColor obstacleColor;
        
        public void SetColor(ColorInGame colorInGame)
        {
            spriteRenderer.sprite = spriteCollectionByColor.GetSpriteByColor(colorInGame);
            obstacleColor.SetObstacleColor(colorInGame);
        }
    }
}