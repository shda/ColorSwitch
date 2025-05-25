using ColorSwitch.Scripts.Obstacles;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ColorSwitch.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private ScoreLabel scoreLabel;
        [SerializeField] private Player player;
        [SerializeField] private GameOverScreen gameOverScreen;

        [SerializeField] private Toggle godModeToggle;

        private void Awake()
        {
            player.Game = this;
            player.OnGameOverEvent = OnGameOverEvent;
            scoreLabel.ResetScore();
        }

        private void OnGameOverEvent()
        {
            if(godModeToggle&& godModeToggle.isOn)
                return;
            
            player.Stop();

            gameOverScreen.gameObject.SetActive(true);
            gameOverScreen.OnRestartGame = () =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            };
        }

        public void OnUpScore(int count)
        {
            scoreLabel.UpScore(count);
        }

        public void ReturnToPool(CrossedTrigger trigger)
        {
            trigger.gameObject.SetActive(false);
        }
    }
}