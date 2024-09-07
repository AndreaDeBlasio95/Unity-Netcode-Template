using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CoinSpawner : NetworkBehaviour
{
    [SerializeField] private RespawningCoin coinPrefab;
    [SerializeField] private int maxCoins = 50;
    [SerializeField] private int coinValue = 10;
    [SerializeField] private Vector2 spawnPosition;
    [SerializeField] private float maxMapSize;
    [SerializeField] private LayerMask layerMask;

    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        for (int i=0; i<maxCoins; i++)
        {
            SpawnCoin();
        }

    }

    private void SpawnCoin ()
    {
        GetSpawnPoint();

        RespawningCoin coinInstance = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        coinInstance.SetValue(coinValue);
        coinInstance.GetComponent<NetworkObject>().Spawn();

        coinInstance.OnCollected += HandleCoinCollected;
    }

    private void HandleCoinCollected(RespawningCoin coin)
    {
        coin.transform.position = GetSpawnPoint();
        coin.Reset();
    }

    private Vector2 GetSpawnPoint ()
    {
        float xValue = Random.Range(-maxMapSize, maxMapSize);
        float yValue = Random.Range(-maxMapSize, maxMapSize);
        spawnPosition = new Vector2(xValue, yValue);
        return spawnPosition;
    }
}
