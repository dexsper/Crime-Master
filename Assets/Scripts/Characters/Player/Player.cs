using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    private PlayerMovement _playerMovement;
    private PlayerCards _playerCards;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerCards PlayerCards => _playerCards;

    [Inject]
    private LevelManager _levelManager;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _levelManager.OnLevelChanged.AddListener(Respawn);
    }

    private void Respawn(Level level)
    {
        transform.position = _spawnPoint.transform.position;
    }
}
