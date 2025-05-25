using UnityEngine;

namespace ColorSwitch.Scripts.Obstacles
{
    public class StarBonusObstacle : CrossedTrigger
    {
        [SerializeField] private int countUpScore = 1;
        public override void OnPlayerTriggerEnter(Player player , Game game)
        {
            game.OnUpScore(countUpScore);
            game.ReturnToPool(this);
        }
    }
}