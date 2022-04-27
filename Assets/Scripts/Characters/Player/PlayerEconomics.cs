
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
    [Inject]
    private Robbery _robbery;

    public int Money => _money;
    public int EarnedMoney { get; private set; }

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _robbery.OnLose.AddListener(RemoveMoney);
        _levelManager.LevelChanged.AddListener((x) => ResetEarned());
    }

    private void ResetEarned()
    {
        EarnedMoney = 0;
    }

    private void RemoveMoney()
    {
        _money -= EarnedMoney;

        ResetEarned();
    }

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
        EarnedMoney -= summ;

    }
    public bool EnoughMoney(int summ) => _money >= summ;
}

