using System;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    [Serializable]
    public class ColoPairToEnum
    {
        [SerializeField] private Color color;
        [SerializeField] private ColorInGame colorInGame;

        public Color Color => color;
        public ColorInGame ColorInGame => colorInGame;
    }
}