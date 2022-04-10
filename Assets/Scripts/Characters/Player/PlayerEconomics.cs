
using UnityEngine;

public class PlayerEconomics : MonoBehaviour
{
    [SerializeField] private int _money;

    public int Money => _money;

    public void Deposit(int summ) => _money += summ;
    public void Take(int summ)
    {
        if (_money < summ)

            throw new System.Exception("Summ can't be more than money.");

        _money -= summ;

    }
    public bool EnoughMoney(int summ) => _money >= summ;
}

