using UnityEngine;

namespace ColorSwitch.Scripts.Obstacles
{
    public class ObstacleColor : CrossedTrigger
    {
        [SerializeField] private ColorInGame colorPass;
        
        public override void OnPlayerTriggerEnter(Player player , Game game)
        {
            if (colorPass != player.CurrentColor)
            {
                player.OnGameOver();;
            }
        }

        public void SetObstacleColor(ColorInGame colorInGame)
        {
            colorPass = colorInGame;
        }
    }
}