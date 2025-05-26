using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    public abstract class PathToLine : MonoBehaviour
    {
        public abstract Vector3 GetPointToPath(float value);
        public abstract void RefreshPath();
    }
    
    [ExecuteInEditMode]
    public class MovingObjectsToPath : MonoBehaviour
    {
        [SerializeField] private bool isMoving;
        [SerializeField] private int countItems = 10;
        [SerializeField] private float movingSpeed = 0.2f;
        [SerializeField] private float startOffset;
        
        [SerializeField] private Path path;
        [SerializeField] private Transform rootMovingObjects;
        [SerializeField] private MovingObject movingObjectPrefab;
        [SerializeField] private MovingObject[] movingObjects;


        private float _currentOffset;
        private float _currentStep;

        private float _oldRang;

        private void Update()
        {
            if (!isMoving)
                return;

            if (!path)
                return;

            _currentStep += Time.deltaTime * movingSpeed;

            CreateMovingObjectsIfNeeded();

            if (Application.isPlaying)
            {
                MovingObjects();
            }
        }

        private void MovingObjects()
        {
            var offset = 1.0f / CountItems();

            for (int i = 0; i < movingObjects.Length; i++)
            {
                var position = path.GetPointOnPath(i * offset + _currentStep);
                movingObjects[i].transform.position = position;
            }
        }

        private int CountItems()
        {
            var colorValues = Enum.GetValues(typeof(ColorInGame));
            var colorCount = colorValues.Length;
            return (countItems / colorCount) * colorCount;
        }

        private void CreateMovingObjectsIfNeeded()
        {
            var colorValues = Enum.GetValues(typeof(ColorInGame));
            int colorCount = colorValues.Length;
            int totalObjects = CountItems();

            if (movingObjectPrefab && movingObjects.Length != totalObjects || _currentOffset != startOffset)
            {
                _currentOffset = startOffset;
                
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

                    var position = path.GetPointOnPath(i * positionStep + startOffset);
                    movingObject.transform.position = position;

                    newMovingObjects.Add(movingObject);
                }

                movingObjects = newMovingObjects.ToArray();
            }
        }
    }
}