using System;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    [Serializable]
    public class SpriteByColor
    {
        [SerializeField] private Sprite sprite;
        [SerializeField] private ColorInGame color;
        
        public Sprite Sprite=> sprite;
        public ColorInGame ColorInGame => color;
    }
}