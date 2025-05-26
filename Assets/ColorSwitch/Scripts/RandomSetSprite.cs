using UnityEngine;

namespace ColorSwitch.Scripts
{
    public class RandomSetSprite : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private Sprite[] sprites;

        private void OnEnable()
        {
            sprite.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}