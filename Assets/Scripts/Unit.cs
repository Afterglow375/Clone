using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace DefaultNamespace
{
    public class Unit : MonoBehaviour
    {
        public BoxCollider2D hitbox;

        void Start()
        {
            Assert.IsNotNull(hitbox);
            StartImpl();
        }

        public Vector3 GetCenter()
        {
            return hitbox.bounds.center;
        }

        protected virtual void StartImpl() {}
    }
}