using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColorSwitch.Scripts.Obstacles
{
    public class ChangeColorObstacle : CrossedTrigger
    {
        [SerializeField] private bool once = true;
        [SerializeField] private ColorInGame setColor;

        private bool _hasColorChanged;

        public override void OnPlayerTriggerEnter(Player player, Game game)
        {
            if (_hasColorChanged && once)
                return;

            _hasColorChanged = true;

            var colorOptions = BaseShape?.GetComponent<OnlyColorSwitchOptions>();

            if (colorOptions)
            {
                if (colorOptions.Colors.Length > 1)
                {
                    SetRandomColor(player, colorOptions.Colors);
                }
                else if (colorOptions.Colors.Length == 1)
                {
                    player.SetColor(colorOptions.Colors[0]);
                }
            }
            else
            {
                var allColors = Enum.GetValues(typeof(ColorInGame)).Cast<ColorInGame>().ToArray();
                SetRandomColor(player, allColors);
            }
        }

        private void SetRandomColor(Player player, ColorInGame[] availableColors)
        {
            ColorInGame newColor;
            do
            {
                newColor = (ColorInGame)availableColors.GetValue(Random.Range(0, availableColors.Length));
            } while (newColor == player.CurrentColor);

            player.SetColor(newColor);
        }
    }
}