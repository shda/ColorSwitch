using ColorSwitch.Scripts.Obstacles;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    public class MovingObject : MonoBehaviour
    {
        [SerializeField] private ColorsStorage colorsStorage;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ObstacleColor obstacleColor;
        
        public void SetColor(ColorInGame colorInGame)
        {
            spriteRenderer.color = colorsStorage.GetColor(colorInGame);
            obstacleColor.SetObstacleColor(colorInGame);
        }
    }
}