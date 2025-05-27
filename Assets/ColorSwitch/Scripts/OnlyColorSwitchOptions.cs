using UnityEngine;

namespace ColorSwitch.Scripts
{
    public class OnlyColorSwitchOptions : MonoBehaviour
    {
        [SerializeField] private ColorInGame[] colors;
        
        public ColorInGame[] Colors => colors;
    }
}