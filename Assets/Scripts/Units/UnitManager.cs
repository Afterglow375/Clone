using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Units
{
    public class UnitManager : Singleton<UnitManager>
    {
        private List<Player> _players = new List<Player>(4);
        private Vector2 _screenDimensions;
        private Camera _camera;

        public float knockbackDuration;

        private void Start()
        {
            _camera = Camera.main;
            float screenHeight = 2f * _camera.orthographicSize;
            float screenWidth = screenHeight * _camera.aspect;
            _screenDimensions = new Vector2(screenWidth, screenHeight);
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player);
        }
        
        public Player GetNearestPlayer(Vector2 position)
        {
            Player closest = null;
            float distance = Mathf.Infinity;
            foreach (Player player in _players)
            {
                Vector2 diff = (Vector2) player.GetCenter() - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = player;
                    distance = curDistance;
                }
            }

            return closest;
        }
        
        public Collider2D GetNearestEnemy(Vector2 position)
        {
            // optimizations...
            // call less often (like every .2 secs + after u kill current closest enemy)
            // use OverlapBoxNonAlloc to avoid creating a new arr every search
            // use KD tree
            Collider2D[] enemies = Physics2D.OverlapBoxAll(_camera.transform.position, _screenDimensions, 0, LayerMaskHelper.EnemyHitboxMask);
            Collider2D closest = null;
            float distance = Mathf.Infinity;
            foreach (Collider2D enemy in enemies)
            {
                Vector2 diff = (Vector2) enemy.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = enemy;
                    distance = curDistance;
                }
            }
            return closest;
        }

        public void AttackPlayer(Player player, float dmg)
        {
            player.TakeDamage(dmg);
        }
    }
}