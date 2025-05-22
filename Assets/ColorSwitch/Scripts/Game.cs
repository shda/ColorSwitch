using System;
using BlockBlast.Scripts.Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ColorSwitch.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private GameOverScreen gameOverScreen;

        private void Awake()
        {
            player.Game = this;
            player.OnGameOverEvent = OnGameOverEvent;
        }

        private void OnGameOverEvent()
        {
            player.Stop();
            
            gameOverScreen.gameObject.SetActive(true);
            gameOverScreen.OnRestartGame = () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            };
        }
    }
}