using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Collections;

public class LeaderboardEntitiesDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;
    private FixedString32Bytes playerName;

    public ulong ClientId { get; private set; }
    public int Coins { get; private set; }

    public void Initialize(ulong clientId, FixedString32Bytes playerName, int coins)
    {
        ClientId = clientId;
        this.playerName = playerName;

        UpdateCoins(coins);
    }

    public void UpdateCoins(int c)
    {
        Coins = c;

        UpdateText();
    }

    private void UpdateText()
    {
        displayText.text = $"1. {playerName} - {Coins}";
    }
}
