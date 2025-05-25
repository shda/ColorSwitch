using UniRx;
using UnityEngine;

namespace ColorSwitch.Scripts.Level
{
    public class ScoreModel : MonoBehaviour
    {
        public IntReactiveProperty Score { get; set; } = new();
    }
}