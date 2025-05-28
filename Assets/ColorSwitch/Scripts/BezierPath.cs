using BezierSolution;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    public class BezierPath : PathAbstract
    {
        [SerializeField] private BezierSpline bezierSpline;
        [SerializeField] private int pathCounts = 30;
        
        private BezierSpline.PointCache _pointCache;
        
        public override Vector3 GetPointOnPath(float value)
        {
            if(_pointCache == null)
                return Vector3.zero;
            
            return _pointCache.GetPoint(value);
        }
        public override void RefreshPath()
        {
            bezierSpline.Refresh();
            _pointCache = bezierSpline.GeneratePointCache(PointCacheFlags.Positions , resolution: pathCounts);
        }
    }
}