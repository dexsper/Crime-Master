using System;
using System.Collections;
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
    [Range(100, 1000)]
    [SerializeField] private float _backForce = 500f;
    [Range(0, 2)]
    [SerializeField] private float _forceTime = .7f;

    private Rigidbody _rigidbody;

    [Inject]
    private IInput _input;

    public bool IsMove { get; private set; }
    public bool IsForced { get; private set; }


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (IsForced == false)
        {
            Vector3 direction = new Vector3(_input.Horizontal * _sideSpeed, 0f, _forwardSpeed);

            _rigidbody.MovePosition(transform.position + direction * Time.fixedDeltaTime);

            IsMove = true;
        }
        else
        {
            IsMove = false;
        }
    }

    public void AddBackForce()
    {
        StartCoroutine(ForceBack());
    }

    private IEnumerator ForceBack()
    {
        IsForced = true;
        _rigidbody.AddForce(-transform.forward * _backForce * Time.deltaTime, ForceMode.Impulse);

        yield return new WaitForSeconds(_forceTime);

        IsForced = false;
    }
}