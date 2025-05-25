using BlockBlast.Scripts.Common;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    public class BaseShape : MonoBehaviour
    {
        [SerializeField] private Transform pointSetStar;
        [SerializeField] private Transform lowerLimit;
        [SerializeField] private Transform upperLimit;
        
        public Vector3 LowerLimitPoint => lowerLimit.position;
        public Vector3 UpperLimitPoint => upperLimit.position;
        public Vector3 PointSetStar => pointSetStar.position;

        private PoolGameObjectsComponent<BaseShape> _pool;

        public void SetPool(PoolGameObjectsComponent<BaseShape> pool)
        {
            _pool = pool;
        }

        public void ReturnModelToPool()
        {
            _pool.Release(this);
        }
    }
}