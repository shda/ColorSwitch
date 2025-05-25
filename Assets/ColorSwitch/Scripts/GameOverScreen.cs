using System;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    public class GameOverScreen : MonoBehaviour
    {
        public Action OnRestartGame;
        
        public void OnPressRestart()
        {
            OnRestartGame?.Invoke();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}