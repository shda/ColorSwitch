using UnityEngine;

namespace ColorSwitch.Scripts.Level
{
    [CreateAssetMenu(menuName = "Create ShapeCollection", fileName = "ShapeCollection", order = 0)]
    public class ShapeCollection : ScriptableObject
    {
        [SerializeField] private BaseShape[] shapes;
        public BaseShape[] Shapes => shapes;
    }
}