using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> moodParticles = new List<ParticleSystem>();
    [SerializeField] private TimedText _textNotify;
    [SerializeField] private Transform _spawnPoint;

    [Header("Audio")]
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _trapedClip;

    private PlayerMovement _playerMovement;
    private PlayerInventory _playerInventory;
    private PlayerSkin _playerSkin;
    private PlayerEconomics _playerEconomics;

    public PlayerEconomics Economics => _playerEconomics;
    public PlayerSkin Skin => _playerSkin;
    public PlayerMovement Movement => _playerMovement;
    public PlayerInventory Inventory => _playerInventory;
    public TimedText TextNotify => _textNotify;
    [Inject]
    private LevelManager _levelManager;


    private void Awake()
    {
        _playerEconomics = GetComponent<PlayerEconomics>();
        _playerInventory = GetComponent<PlayerInventory>();
        _playerSkin = GetComponent<PlayerSkin>();
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ITrap trap))
        {
            trap.Activate(this);

            Vector3 dir = transform.position - other.transform.position;
            dir.x = 0;
            dir.y = 0;
            dir.Normalize();

            if(_source != null && _trapedClip != null)
            {
                _source.PlayOneShot(_trapedClip);
            }

            StartCoroutine(_playerMovement.Force(dir, 7f, 2f));
        }
    }
}
