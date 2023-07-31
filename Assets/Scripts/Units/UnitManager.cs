using UnityEngine;
using Utils;

namespace Units
{
    public class UnitManager : Singleton<UnitManager>
    {
        [SerializeField] private Player _player;
        
        public Vector3 GetPlayerPivot()
        {
            return _player.transform.position;
        }
        
        public Vector3 GetPlayerCenter()
        {
            return _player.GetCenter();
        }
        
        public Vector3 GetClosestEnemyPosition()
        {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            
            Enemy closest = null;
            float distance = Mathf.Infinity;
            foreach (Enemy enemy in enemies)
            {
                Vector3 diff = enemy.GetCenter() - _player.GetCenter();
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = enemy;
                    distance = curDistance;
                }
            }
            return closest.GetCenter();
        }
    }
}