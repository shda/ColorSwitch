using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColorSwitch.Scripts.Obstacles
{
    public class ChangeColorObstacle : CrossedTrigger
    {
        [SerializeField] private bool once = true;

        private ColorInGame[] _colorOptions;
        private bool _hasColorChanged;

        public override void OnPlayerTriggerEnter(Player player, Game game)
        {
            if (_hasColorChanged && once)
                return;

            _hasColorChanged = true;
            if (_colorOptions != null)
            {
                if (_colorOptions.Length > 1)
                {
                    SetRandomColor(player, _colorOptions);
                }
                else if (_colorOptions.Length == 1)
                {
                    player.SetColor(_colorOptions[0]);
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

        public void SetColorOptions(OnlyColorSwitchOptions colorOptions)
        {
            _colorOptions = colorOptions?.Colors;
        }
    }
}