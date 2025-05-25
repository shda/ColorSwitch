using System;
using ColorSwitch.Scripts.Level;
using TMPro;
using UniRx;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    public class ScoreLabel : MonoBehaviour
    {
        [SerializeField] private ScoreModel scoreModel;
        [SerializeField] private TextMeshProUGUI scoreLabel;

        private void Awake()
        {
            scoreModel.Score.Subscribe(OnChangeScore).AddTo(this);
        }

        private void OnChangeScore(int score)
        {
            scoreLabel.text = score.ToString();
        }
        
        public void ResetScore()
        {
            scoreModel.Score.Value = 0;
        }

        public void UpScore(int count)
        {
            scoreModel.Score.Value += count;
        }
    }
}