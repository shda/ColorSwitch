using System;
using System.Collections.Generic;
using BlockBlast.Scripts.Common;
using ColorSwitch.Scripts.Obstacles;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColorSwitch.Scripts.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private ScoreModel scoreModel;
        [SerializeField] private Player player;
        [SerializeField] private float minDistanceCreateModel = 10f;
        [SerializeField] private float maxDistanceReturnModelToPool = 40f;
        [SerializeField] private int createStarAllShapes = 3;
        [SerializeField] private int changeColorAllShapes = 2;
        
        [SerializeField] private Transform startPointGenerator;
        
        [SerializeField] private ShapesPool easyShapesPool;
        [SerializeField] private ShapesPool mediumShapesPool;
        [SerializeField] private ShapesPool hardShapesPool;
        
        [SerializeField] private PoolGameObjectsComponent<ChangeColorObstacle> changeColorShapePool;
        [SerializeField] private PoolGameObjectsComponent<StarBonusObstacle> startShapePool;
        
        private HashSet<BaseShape> _shapesShow = new();
        
        private Vector3 _upPointModel;
        private Vector3 _downPointModel;
        
        private int _currentShape = 0;
        
        private void Awake()
        {
            _upPointModel = startPointGenerator.position;
            InitPools();
        }
        
        private void Update()
        {
            if (Vector3.Distance(player.transform.position , _upPointModel) < minDistanceCreateModel)
            {
                var model = CreateShape(_upPointModel);
                _upPointModel = model.UpperLimitPoint;
                _shapesShow.Add(model);

                _currentShape++;

                if (_currentShape % createStarAllShapes == 0)
                {
                    var star = startShapePool.Get();
                    star.transform.position = model.PointSetStar;
                }

                if (_currentShape % changeColorAllShapes == 0)
                {
                    var changeColor = changeColorShapePool.Get();
                    changeColor.transform.position = model.LowerLimitPoint;
                }
            }
            
            foreach (var shape in _shapesShow)
            {
                if (player.transform.position.y - shape.transform.position.y > maxDistanceReturnModelToPool)
                {
                    shape.ReturnModelToPool();
                    _shapesShow.Remove(shape);
                    return;
                }
            }
        }

        private BaseShape CreateShape(Vector3 minPosition)
        {
            var easyProp = 0;
            var mediumProp = 0;
            var hardProp = 0;

            GetProbabilityByScore(ref easyProp, ref mediumProp, ref hardProp);
            
            var pointEnd = startPointGenerator;
            BaseShape model = null;
            
            var random = Random.Range(0, 100);
            if (random < easyProp)
            {
                model = CreateModel(easyShapesPool , minPosition);
            }
            else if (random < easyProp + mediumProp)
            {
                model = CreateModel(mediumShapesPool , minPosition);
            }
            else
            {
                model = CreateModel(hardShapesPool , minPosition);
            }

            return model;
        }

        private BaseShape CreateModel(ShapesPool pool , Vector3 endPointGenerator)
        {
            var shape = pool.GetRandomShape();
            MovingShapeToPoint(shape , endPointGenerator);
            return shape;
        }

        private void GetProbabilityByScore(ref int easyProp, ref int mediumProp, ref int hardProp)
        {
            var score = scoreModel.Score.Value;
            switch (score)
            {
                case <= 5:
                    easyProp = 75;
                    mediumProp = 25;
                    hardProp = 0;
                    break;
                case <= 10:
                    easyProp = 50;
                    mediumProp = 25;
                    hardProp = 25;
                    break;
                case <= 15:
                    easyProp = 25;
                    mediumProp = 50;
                    hardProp = 25;
                    break;
                case <= 20:
                    easyProp = 0;
                    mediumProp = 50;
                    hardProp = 50;
                    break;
                default:
                    easyProp = 0;
                    mediumProp = 25;
                    hardProp = 75;
                    break;
            }
        }

        private void MovingShapeToPoint(BaseShape shape , Vector3 pointEnd)
        {
            shape.transform.position = pointEnd;
            shape.transform.position -= shape.LowerLimitPoint - shape.transform.position;
        }
        
        private void InitPools()
        {
            easyShapesPool.InitPool();
            mediumShapesPool.InitPool();
            hardShapesPool.InitPool();
            
            changeColorShapePool.Init();
            startShapePool.Init();
        }
    }
}