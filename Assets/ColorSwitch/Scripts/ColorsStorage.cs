using System.Linq;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    [CreateAssetMenu(menuName = "Create ColorsByEnum", fileName = "ColorsByEnum", order = 0)]
    public class ColorsStorage : ScriptableObject
    {
        [SerializeField] private ColoPairToEnum[] colors;

        public Color GetColor(ColorInGame colorInGame)
        {
            var colorPair = colors.FirstOrDefault(x => x.ColorInGame == colorInGame);
            if (colorPair != null)
            {
                return colorPair.Color;
            }
            
            return Color.black;
        }
    }
}