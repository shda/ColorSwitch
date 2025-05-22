using UnityEngine;

namespace ColorSwitch.Scripts.Obstacles
{
    public abstract class CrossedTrigger : MonoBehaviour
    {
        public abstract void OnPlayerTriggerEnter(Player other , Game game);
    }
}