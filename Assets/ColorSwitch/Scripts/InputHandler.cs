using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace ColorSwitch.Scripts
{
    public class InputHandler : MonoBehaviour
    {
        public Action OnTap;
            
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                OnTap?.Invoke();
            }
        }
    }
}