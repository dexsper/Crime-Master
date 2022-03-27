using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    private PlayerMovement _playerMovement;

    public PlayerEconomics Economics => _playerEconomics;
    public PlayerMovement Movement => _playerMovement;
    public PlayerInventory Inventory => _playerInventory;

    [Inject]
    private LevelManager _levelManager;

    [Inject]
    private PlayerInventory _playerInventory;

    [Inject]
    private PlayerEconomics _playerEconomics;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _levelManager.LevelChanged.AddListener(Respawn);
    }

    public void Respawn(Level level)
    {
        transform.position = _spawnPoint.transform.position;
        Movement.enabled = true;
    }
}
