using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColorSwitch.Scripts.Obstacles
{
    public class ChangeColorObstacle : CrossedTrigger
    {
        [SerializeField] private bool once = true;
        [SerializeField] private bool setRandomColor = true;
        [SerializeField] private ColorInGame setColor;

        private bool _isChanged;
        public override void OnPlayerTriggerEnter(Player player , Game game)
        {
            if(_isChanged && once)
                return;
            
            _isChanged = true;
            
            var color = setColor;
            if (setRandomColor)
            {
                var values = Enum.GetValues(typeof(ColorInGame));
                
                do 
                {
                    color = (ColorInGame)values.GetValue(Random.Range(0, values.Length));
                }
                while(color == player.CurrentColor);
                player.SetColor(color);
            }
   
            player.SetColor(color);
        }
    }
}