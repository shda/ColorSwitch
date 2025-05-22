using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    [ExecuteInEditMode]
    public class PathMoving : MonoBehaviour
    {
        [SerializeField] private Path path;

        [SerializeField] private Transform rootMovingObjects;
        [SerializeField] private int countItems = 10;
        [SerializeField] private MovingObject movingObjectPrefab;
        [SerializeField] private MovingObject[]  movingObjects;
        [SerializeField] private float mod = 0.2f;
        
        private float _currentStep;
        
        private float _oldRang;
        private void Update()
        {
            if(!path)
                return;

            _currentStep += Time.deltaTime * mod;
            
            CreateMovingObjectsIfNeeded();

            if (Application.isPlaying)
            {
                MovingObjects();
            }
        }

        private void MovingObjects()
        {
            var offset = 1.0f / countItems;
                
            for (int i = 0; i < movingObjects.Length; i++)
            {
                var position = path.GetPointOnPath(i * offset + _currentStep);
                movingObjects[i].transform.position = position;
            }
        }

      private void CreateMovingObjectsIfNeeded()
        {
            var colorValues = Enum.GetValues(typeof(ColorInGame));
            int colorCount = colorValues.Length;
            int totalObjects = (countItems / colorCount) * colorCount;
        
            if (movingObjectPrefab && movingObjects.Length != totalObjects)
            {
                var children = rootMovingObjects.Cast<Transform>().ToArray();
        
                foreach (var child in children)
                {
                    DestroyImmediate(child.gameObject);
                }
        
                float positionStep = 1.0f / totalObjects;
                int objectsPerColor = totalObjects / colorCount;
        
                var newMovingObjects = new List<MovingObject>();
                for (int i = 0; i < totalObjects; i++)
                {
                    var movingObject = Instantiate(movingObjectPrefab);
                    movingObject.transform.SetParent(rootMovingObjects);
        
                    int colorIndex = (i == 0) ? 0 : i / objectsPerColor;
                    movingObject.SetColor((ColorInGame)colorValues.GetValue(colorIndex));
        
                    var position = path.GetPointOnPath(i * positionStep);
                    movingObject.transform.position = position;
        
                    newMovingObjects.Add(movingObject);
                }
        
                movingObjects = newMovingObjects.ToArray();
            }
        }
    }
}