using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Speed Settings")]
    [Range(1, 10)]
    [SerializeField] private float _forwardSpeed = 5f;

    [Range(1, 10)]
    [SerializeField] private float _sideSpeed = 1.2f;

    [Header("Card Force")]
    [Range(1, 10)] 
    [SerializeField] private float _backForce = 1f;

    private Rigidbody _rigidbody;

    [Inject]
    private IInput _input;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = new Vector3(_input.Horizontal * _sideSpeed, 0f, _forwardSpeed);

        _rigidbody.MovePosition(transform.position + direction * Time.fixedDeltaTime);
    }

    public void AddForce(Vector3 from)
    {
        Vector3 direction = (from - transform.position).normalized;

        _rigidbody.AddForce(direction * _backForce); 
    }
}
