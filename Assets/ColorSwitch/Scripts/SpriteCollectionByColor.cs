using System;
using System.Linq;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    [Serializable]
    public class SpriteCollectionByColor
    {
        [SerializeField] private SpriteByColor[]  sprites;

        public Sprite GetSpriteByColor(ColorInGame color)
        {
            return sprites.First(x => x.ColorInGame == color).Sprite;
        }
    }
}