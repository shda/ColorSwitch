using System;
using System.Collections.Generic;
using System.Linq;
using BlockBlast.Scripts.Common;
using BlockBlast.Scripts.Common.Extensions;
using UnityEngine;
using UnityEngine.Pool;

namespace ColorSwitch.Scripts.Level
{
    [Serializable]
    public class ShapesPool
    {
        [SerializeField] private int initSize = 1;
        [SerializeField] private ShapeCollection shapeCollection;

        private Dictionary<string , PoolGameObjectsComponent<BaseShape>> _pools = new ();

        private Queue<BaseShape> _shapeQueue = new();

        public void InitPool()
        {
            foreach (var shape in shapeCollection.Shapes)
            {
                var pool = new PoolGameObjectsComponent<BaseShape>();
                _pools[shape.name] = pool;
                
                pool.InitPool(shape , initSize);
            }
        }
        
        public BaseShape GetRandomShape()
        {
            var shape = ExtractRandomShapePool();
            var newShape = _pools[shape.name].Get();
            newShape.SetPool(_pools[shape.name]);
            newShape.name = shape.name;
            return newShape;
        }

        public void ReturnRandomShape(BaseShape shape)
        {
            var newShape = _pools[shape.name].Get();
            _pools[shape.name].Release(newShape);
        }

        private BaseShape ExtractRandomShapePool()
        {
            if (!_shapeQueue.Any())
            {
                var list = ListPool<BaseShape>.Get();
                
                list.AddRange(shapeCollection.Shapes);
                list.Shuffle();
                list.ForEach(x => _shapeQueue.Enqueue(x));
                
                ListPool<BaseShape>.Release(list);
            }
            
            return  _shapeQueue.Dequeue();
        }
    }
}