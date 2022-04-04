using System;
using UnityEngine;

namespace Level.Input
{
    public class InputService : MonoBehaviour
    {
        public event Action<Vector3> OnMovementDirection;
        private Vector3 _direction;
        private void Update()
        {
            _direction.x = UnityEngine.Input.GetAxis("Horizontal");
            _direction.z = UnityEngine.Input.GetAxis("Vertical");
        }
        private void FixedUpdate()
        {
            _direction.Normalize();
            OnMovementDirection?.Invoke(_direction);
        }
    }
}
