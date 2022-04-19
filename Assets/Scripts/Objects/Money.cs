using System.Collections;
using UnityEngine;

public class Money : BaseInteractable
{
    [Header("Money")]
    [Range(1, 100)]
    [SerializeField] private int _amount;

    [Header("Magnet Settings")]
    [Range(0, 3)]
    [SerializeField] private float _distance = 1;

    [Range(0, 2)]
    [SerializeField] private float _moveDuration = 1f;


    [Header("Debug")]
    [SerializeField] private bool _debug;
    [SerializeField] private Color _debugColor = Color.white;

    private bool _isMoving = false;



    protected override void Update()
    {
        base.Update();

        if (_isMoving == false && Vector3.Distance(_player.transform.position, transform.position) <= _distance)
        {
            _isMoving = true;

            StartCoroutine(MoveTo(_player.transform, _moveDuration));
        }
    }

    public IEnumerator MoveTo(Transform target, float duration)
    {
        float elapsedTime = 0;
        float ratio = elapsedTime / duration;

        Vector3 startPos = transform.position;

        while (ratio < 1f)
        {
            elapsedTime += Time.deltaTime;
            ratio = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPos, target.position, ratio);
            yield return null;
        }

        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }

    public override void Use()
    {
        _player.Economics.Deposit(_amount);
        SFX.Instance.PlayMoneyTake();
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        if (_debug)
        {
            Gizmos.color = _debugColor;
            Gizmos.DrawCube(transform.position, new Vector3(_distance, _distance, _distance));
        }
    }
}
