using System;
using System.Collections.Generic;
using BlockBlast.Scripts.Common;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ColorSwitch.Scripts
{
    public class Clouds : MonoBehaviour
    {
        [SerializeField] private PoolGameObjectsComponent<Transform> poolClouds;
        [SerializeField] private Transform player;
        [SerializeField] private float minDistanceMakeClouds = 10;
        [SerializeField] private float stepToClouds = 2;
        [SerializeField] private Vector2 rangeOffset;

        private HashSet<Transform> cloudsInScene = new();
        
        private float _upPointModel;
        public void Awake()
        {
            _upPointModel = player.transform.position.y - minDistanceMakeClouds;
        }

        private void Update()
        {
            CreateNewClouds();
            RemoveCloudsForDistance();
        }

        private void RemoveCloudsForDistance()
        {
            foreach (var cloud in cloudsInScene)
            {
                if (cloud.transform.position.y < player.transform.position.y - minDistanceMakeClouds)
                {
                    poolClouds.Release(cloud);
                    cloudsInScene.Remove(cloud);
                    break;
                }
            }
        }

        private void CreateNewClouds()
        {
            while (_upPointModel < player.transform.position.y + minDistanceMakeClouds)
            {
                var cloud = poolClouds.Get();
                cloud.transform.position = new Vector3(
                    Random.Range(-rangeOffset.x , rangeOffset.x), _upPointModel + stepToClouds , 0);
                _upPointModel += stepToClouds;
                cloudsInScene.Add(cloud);
            }
        }
    }
}