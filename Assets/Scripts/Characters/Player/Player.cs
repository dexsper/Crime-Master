using UnityEngine;
using Zenject;
using System.Collections.Generic;

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

    [SerializeField] private List<ParticleSystem> moodParticles = new List<ParticleSystem>();


    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _levelManager.LevelChanged.AddListener(Respawn);
    }

    public void Respawn(Level level)
    {
        transform.position = _spawnPoint.transform.position;
    }

    public void PlayBadMoodParticles()
    {
        moodParticles[0].Play();
    }

    public void PlayGoodMoodParticles()
    {
        moodParticles[1].Play();
    }

}
