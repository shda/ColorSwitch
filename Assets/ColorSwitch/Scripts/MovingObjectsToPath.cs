using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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
        [SerializeField] private bool isInfinityOffest = false;

        [SerializeField] private float max = 0.9f;

        [SerializeField] private PathAbstract path;
        [SerializeField] private Transform rootMovingObjects;
        [SerializeField] private MovingObject movingObjectPrefab;
        [SerializeField] private MovingObject[] movingObjects;
        
        private float _currentStep;
        
        private void Awake()
        {
            
        }

        private void OnEnable()
        {
            Delay();
        }


        private async void Delay()
        {
            await UniTask.Delay(100);
            path.RefreshPath();
        }

        private void Update()
        {
            if (!isMoving)
                return;

            if (!path)
                return;

            _currentStep += Time.deltaTime * movingSpeed;
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
            var totalObjects = CountItems();
            var colorValues = CreateItems(totalObjects);
            
            path.RefreshPath();
            
            var children = rootMovingObjects.Cast<Transform>().ToArray();

            foreach (var child in children)
            {
                DestroyImmediate(child.gameObject);
            }

            float positionStep = 1.0f / totalObjects;

            var newMovingObjects = new List<MovingObject>();
            for (int i = 0; i < totalObjects; i++)
            {
                var movingObject = Instantiate(movingObjectPrefab);
                movingObject.transform.SetParent(rootMovingObjects);
                
                movingObject.SetColor(colorValues[i]);

                var position = path.GetPointOnPath(i * positionStep + startOffset);
                Debug.Log(position);
                movingObject.transform.position = position;

                newMovingObjects.Add(movingObject);
            }

            movingObjects = newMovingObjects.ToArray();
        }

        private ColorInGame[] CreateItems(int totalObjects)
        {
            var colorValues = Enum.GetValues(typeof(ColorInGame)).Cast<ColorInGame>().ToArray();
            var colorCount = colorValues.Length;

            var colors = new List<ColorInGame>();
            var objectsPerColor = totalObjects / colorCount;

            if (isInfinityOffest)
            {
                var cou = objectsPerColor / 2;
                int currentIndex = 0;
                for (var i = 0; i < totalObjects; i++)
                {
                    //int colorIndex = currentIndex % cou;
                    var color = (ColorInGame)colorValues.GetValue(currentIndex);
                    colors.Add(color);
                    if (i > 0 && i % cou == 0)
                    {
                        currentIndex++;

                        if (currentIndex >= colorValues.Length)
                            currentIndex = 0;
                    }
                }
            }
            else
            {
                for (var i = 0; i < totalObjects; i++)
                {
                    int colorIndex = (i == 0) ? 0 : i / objectsPerColor;
                    colors.Add((ColorInGame)colorValues.GetValue(colorIndex));
                }
            }
            
            return colors.ToArray();
        }
    }
}