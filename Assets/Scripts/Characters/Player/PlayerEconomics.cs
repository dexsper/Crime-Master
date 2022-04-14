
using System;
using UnityEngine;
using Zenject;

public class PlayerEconomics : MonoBehaviour
{
    [SerializeField] private Color _earnedColor = Color.green;

    [SerializeField] private int _money;

    [Inject]
    private LevelManager _levelManager;
    private Player _player;
    
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _levelManager.LevelChanged.AddListener(ShowEarned);
    }

    private void ShowEarned(Level level)
    {
        _player.TextNotify.Show($"+ {EarnedMoney} $", _earnedColor);
        EarnedMoney = 0;
    }

    public int Money => _money;
    public int EarnedMoney { get; private set; }

    public void Deposit(int summ)
    {
        _money += summ;
        EarnedMoney += summ;
    }
    public void Take(int summ)
    {
        if (_money < summ)

            throw new System.Exception("Summ can't be more than money.");

        _money -= summ;

    }
    public bool EnoughMoney(int summ) => _money >= summ;
}

