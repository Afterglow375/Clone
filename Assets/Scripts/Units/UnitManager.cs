using System;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace Units
{
    public class UnitManager : Singleton<UnitManager>
    {
        [SerializeField] private Player _player;
        
        private Vector2 _screenDimensions;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            float screenHeight = 2f * _camera.orthographicSize;
            float screenWidth = screenHeight * _camera.aspect;
            _screenDimensions = new Vector2(screenWidth, screenHeight);
        }

        public Vector3 GetPlayerPivot()
        {
            return _player.transform.position;
        }
        
        public Vector3 GetPlayerCenter()
        {
            return _player.GetCenter();
        }
        
        public bool IsPlayerFacingLeft()
        {
            return _player.IsFacingLeft();
        }
        
        public Collider2D GetClosestEnemy()
        {
            // optimizations...
            // call less often (like every .2 secs + after u kill current closest enemy)
            // use OverlapBoxNonAlloc to avoid creating a new arr every search
            Collider2D[] enemies = Physics2D.OverlapBoxAll(_camera.transform.position, _screenDimensions, 0, LayerMaskHelper.EnemyHitboxMask);
            Collider2D closest = null;
            float distance = Mathf.Infinity;
            foreach (Collider2D enemy in enemies)
            {
                Vector3 diff = enemy.transform.position - _player.GetCenter();
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = enemy;
                    distance = curDistance;
                }
            }
            return closest;
        }
    }
}