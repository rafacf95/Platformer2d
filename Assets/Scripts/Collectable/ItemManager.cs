using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using TMPro;

public class ItemManager : Singleton<ItemManager>
{
    public SOInt coins;
    public TextMeshProUGUI coinText;

    private void Reset()
    {
        coins.value = 0;
        // coinText.text = "x " + ItemManager.Instance.coins.value.ToString();
    }

    public void AddCoin(int amount = 1)
    {
        coins.value += amount;
        // coinText.text = "x " + ItemManager.Instance.coins.value.ToString();
    }

    void Start()
    {
        Reset();
    }
}
