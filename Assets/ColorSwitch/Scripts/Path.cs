using System.Collections.Generic;
using UnityEngine;

namespace ColorSwitch.Scripts
{
    public class Path : PathAbstract
    {
        [SerializeField] private bool loop = true;
        [SerializeField] private List<Transform> waypoints;

        public List<Transform> Waypoints => waypoints;

        private float[] _segmentLengths;
        private float _totalPathLength;

        public override void RefreshPath()
        {
            // Рассчитываем длины всех сегментов
            _segmentLengths = new float[waypoints.Count];
            _totalPathLength = 0f;

            for (int i = 0; i < waypoints.Count; i++)
            {
                int nextIndex = (i + 1) % waypoints.Count;
                _segmentLengths[i] = Vector3.Distance(waypoints[i].position, waypoints[nextIndex].position);
                _totalPathLength += _segmentLengths[i];
            }
        }

        public override Vector3 GetPointOnPath(float t)
        {
            if (_segmentLengths == null)
                RefreshPath();

            if (waypoints.Count < 2)
                return transform.position;

            t = Mathf.Repeat(t, 1f); // Зацикливаем значение между 0 и 1

            // Особый случай - точно первая точка
            if (t == 0f)
                return waypoints[0].position;

            float targetLength = t * _totalPathLength;
            float accumulatedLength = 0f;

            for (int i = 0; i < _segmentLengths.Length; i++)
            {
                if (accumulatedLength + _segmentLengths[i] >= targetLength)
                {
                    float segmentProgress = (targetLength - accumulatedLength) / _segmentLengths[i];
                    int nextIndex = (i + 1) % waypoints.Count;
                    return Vector3.Lerp(waypoints[i].position, waypoints[nextIndex].position, segmentProgress);
                }

                accumulatedLength += _segmentLengths[i];
            }

            return waypoints[0].position; // fallback
        }
        
        private void OnDrawGizmos()
        {
            if (waypoints == null || waypoints.Count < 2) return;

            Gizmos.color = Color.green;
            for (var i = 0; i < waypoints.Count - 1; i++)
            {
                if (waypoints[i] && waypoints[i + 1])
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }

            if (loop)
            {
                if (waypoints.Count > 1 && waypoints[0] && waypoints[waypoints.Count - 1])
                {
                    Gizmos.DrawLine(waypoints[waypoints.Count - 1].position, waypoints[0].position);
                }
            }
        }
        
    }
}