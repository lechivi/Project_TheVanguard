using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoin : PlayerAbstract
{
    [SerializeField] private int currentCoint = 1000;
    public int CurrentCoint { get { return currentCoint; } }

    public void SubtractCoin(int amount)
    {
        currentCoint -= amount;
    }

    public void AddCoin(int amount)
    {
        currentCoint += amount;
    }
}
