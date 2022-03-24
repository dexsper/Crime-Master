using UnityEngine;
using Zenject;

public abstract class BaseSpawner : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] protected Transform _start;
    [SerializeField] protected Transform _end;

    [Header("Debug")]
    [SerializeField] protected bool _debug;
    [SerializeField] protected float _sphereSize = .2f;
    [SerializeField] protected Color _lineColor = Color.white;
    [SerializeField] protected Color _startColor = Color.green;
    [SerializeField] protected Color _endColor = Color.red;
    [SerializeField] protected Color _spacingColor = Color.blue;

    [Inject]
    protected LevelManager _levelManager;

    [Inject]
    protected DiContainer _container;


    protected virtual void Awake()
    {
        Spawn(_levelManager.CurrentLevel);
    }
    protected abstract void Spawn(Level level);

    private void OnDrawGizmos()
    {
        if (_start != null && _end != null && _debug)
        {
            Debug.DrawLine(_start.position, _end.position, _lineColor);

            Gizmos.color = _startColor;
            Gizmos.DrawSphere(_start.position, _sphereSize);

            Gizmos.color = _endColor;
            Gizmos.DrawSphere(_end.position, _sphereSize);
        }
    }
}