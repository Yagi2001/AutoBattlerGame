using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldManager : MonoBehaviour
{
    private int _gold;
    [SerializeField] private TextMeshProUGUI _goldText;
    void Start()
    {
        _gold = 100;
        UpdateGoldUI();
    }

    private bool CanAfford(int cost)
    {
        if (_gold >= cost)
            return true;
        else
            return false;
    }
    public void DecreaseGold(int cost)
    {
        if (CanAfford( cost ))
        {
            _gold -= cost;
            UpdateGoldUI();
        }
    }
    private void UpdateGoldUI()
    {
        _goldText.text = _gold.ToString();
    }
}
