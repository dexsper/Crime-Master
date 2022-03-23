using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;

    private PlayerMovement _playerMovement;
    private PlayerInventory _playerInventory;
    private PlayerEconomics _playerEconomics;

    public PlayerEconomics Economics => _playerEconomics;
    public PlayerMovement Movement => _playerMovement;
    public PlayerInventory Inventory => _playerInventory;

    [Inject]
    private LevelManager _levelManager;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInventory = GetComponent<PlayerInventory>();
        _playerEconomics = GetComponent<PlayerEconomics>();

        _levelManager.OnLevelChanged.AddListener(Respawn);
    }

    private void Respawn(Level level)
    {
        transform.position = _spawnPoint.transform.position;
    }
}
