using System;
using UnityEngine;

namespace ColorSwitch.Scripts.Obstacles
{
    public abstract class CrossedTrigger : MonoBehaviour
    {
        public abstract void OnPlayerTriggerEnter(Player other , Game game);
        protected BaseShape BaseShape { get; private set; }
        
        private void Awake()
        {
            BaseShape = FindComponentInParents<BaseShape>(transform);
        }

        private T FindComponentInParents<T>(Transform obj) where T : MonoBehaviour
        {
            var ret = obj.GetComponentInParent<T>();
            if(ret)
            {
                return ret;
            }
            
            if (obj.transform.parent)
            {
                return FindComponentInParents<T>(obj.transform.parent);
            }
            return null;
        }
    }
}