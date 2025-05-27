using System;
using System.Collections.Generic;
using System.Linq;
using BlockBlast.Scripts.Common.Extensions;
using TriInspector;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    [ExecuteInEditMode]
    public class MovingObjectsToPath : MonoBehaviour
    {
        [SerializeField] private bool isMoving;
        [SerializeField] private int countItems = 10;
        [SerializeField] private float movingSpeed = 0.2f;
        [SerializeField] private float startOffset;
        
        [SerializeField] private PathAbstract path;
        [SerializeField] private Transform rootMovingObjects;
        [SerializeField] private MovingObject movingObjectPrefab;
        [SerializeField] private MovingObject[] movingObjects;


       // private float _currentOffset;
        private float _currentStep;

       // private float _oldRang;

        private void Awake()
        {
            path.RefreshPath();
        }

        private void Update()
        {
            if (!isMoving)
                return;

            if (!path)
                return;

            _currentStep += Time.deltaTime * movingSpeed;

           // CreateMovingObjectsIfNeeded();

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

        [Button]
        private void UpdateModel()
        {
            CreateMovingObjectsIfNeeded(true);
        }

        private void CreateMovingObjectsIfNeeded(bool force = false)
        {
            var colorValues = Enum.GetValues(typeof(ColorInGame)).Cast<ColorInGame>().ToArray();
            colorValues.Shuffle();
            
            int colorCount = colorValues.Length;
            int totalObjects = CountItems();

           // if (movingObjectPrefab && movingObjects.Length != totalObjects || !Mathf.Approximately(_currentOffset, startOffset) || force)
            {
                path.RefreshPath();
                
               // _currentOffset = startOffset;
                
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
                    //MovingObject movingObject = (MovingObject)PrefabUtility.InstantiatePrefab(movingObjectPrefab, rootMovingObjects);
                    
                    var movingObject = Instantiate(movingObjectPrefab);
                    movingObject.transform.SetParent(rootMovingObjects);

                    int colorIndex = (i == 0) ? 0 : i / objectsPerColor;
                    movingObject.SetColor((ColorInGame)colorValues.GetValue(colorIndex));

                    var position = path.GetPointOnPath(i * positionStep + startOffset);
                    Debug.Log(position);
                    movingObject.transform.position = position;

                    newMovingObjects.Add(movingObject);
                }

                movingObjects = newMovingObjects.ToArray();
            }
        }
    }
}