using UnityEngine;
using Zenject;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Base Settings")]
    [Range(1, 10)]
    [SerializeField] private float _forwardSpeed = 5f;
    [Range(1, 3)]
    [SerializeField] private float _sideSpeed = 1.2f;

    private Rigidbody _rigidbody;

    [Inject]
    private IInput _input;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 direction = new Vector3(_input.Horizontal * _sideSpeed, 0f, _forwardSpeed);

        transform.Translate(direction * Time.deltaTime);
    }
}
