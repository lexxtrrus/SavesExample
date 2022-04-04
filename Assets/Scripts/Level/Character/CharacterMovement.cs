using Level.Input;
using UnityEngine;

namespace Level.Character
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        private InputService _input;
    
        public void Init(InputService inputService)
        {
            _input = inputService;
            _input.OnMovementDirection += Movement;
        }

        private void OnDestroy()
        {
            _input.OnMovementDirection -= Movement;
        }

        private void Movement(Vector3 direction)
        {
            _rigidbody.MovePosition(_rigidbody.transform.position + direction * 5f * Time.deltaTime);
        }
    }
}
