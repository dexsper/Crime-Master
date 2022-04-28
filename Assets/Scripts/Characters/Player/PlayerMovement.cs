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

    [Header("Audio")]
    [SerializeField] private AudioClip _walkClip;
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _trapedClip;
    [Range(0, 1f)]
    [SerializeField] private float _walkClipDelay;

    [Header("Trap")]
    [Range(1, 10)]
    [SerializeField] private float _trapPower = 7;
    [Range(1, 3)]
    [SerializeField] private float _forceTime = 2f;
    [SerializeField] private ParticleSystem _sparksParticle;

    private Rigidbody _rigidbody;

    [Inject]
    private IInput _input;

    public bool IsMove { get; private set; }
    public bool IsForced { get; private set; }


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    float time = 0;

    private void FixedUpdate()
    {
        if (IsForced == false)
        {
            Vector3 direction = new Vector3(_input.Horizontal * _sideSpeed, 0f, _forwardSpeed);

            _rigidbody.MovePosition(transform.position + direction * Time.fixedDeltaTime);

            time += Time.fixedDeltaTime;

            if(time > _walkClipDelay)
            {
                _source.PlayOneShot(_walkClip);
                time = 0f;
            }

            IsMove = true;
        }
        else
        {
            IsMove = false;
        }
    }

    public void TrapForce(Vector3 pos)
    {
        Vector3 dir = transform.position - pos;
        dir.x = 0;
        dir.y = 0;
        dir.Normalize();

        if (_source != null && _trapedClip != null)
        {
            _source.PlayOneShot(_trapedClip);
        }

        if (_sparksParticle != null)
            _sparksParticle.Play(true);

        StartCoroutine(Force(dir, _trapPower, _forceTime));

    }

    public IEnumerator Force(Vector3 direction, float power, float t)
    {
        IsForced = true;

        _rigidbody.velocity = Vector3.zero;

        _rigidbody.AddForce(direction * power, ForceMode.Impulse);

        yield return new WaitForSeconds(t);

        IsForced = false;
    }
}