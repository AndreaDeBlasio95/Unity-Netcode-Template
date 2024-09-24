using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RespawnHandler : NetworkBehaviour
{
    [SerializeField] private NetworkObject playerPrefab;


    public override void OnNetworkSpawn()
    {
        if (!IsServer) { return; }

        TankPlayer[] players = FindObjectsByType<TankPlayer>(FindObjectsSortMode.None);
        foreach(TankPlayer player in players)
        {
            HandlePlayerSpawn(player);
        }

        TankPlayer.OnPlayerSpawned += HandlePlayerSpawn;
        TankPlayer.OnPlayerDespawned += HandlePlayerDespawn;
    }


    public override void OnNetworkDespawn()
    {
        if (!IsServer) { return; }

        TankPlayer.OnPlayerSpawned -= HandlePlayerSpawn;
        TankPlayer.OnPlayerDespawned -= HandlePlayerDespawn;
    }

    private void HandlePlayerSpawn(TankPlayer player)
    {
        player.Health.OnDie += (health) => HandlePlayerDie(player);
    }

    private void HandlePlayerDespawn(TankPlayer player)
    {
        player.Health.OnDie -= (health) => HandlePlayerDie(player);
    }

    private void HandlePlayerDie(TankPlayer player)
    {
        Destroy(player.gameObject);

        StartCoroutine(RespawnPlayer(player.OwnerClientId));
    }

    private IEnumerator RespawnPlayer(ulong ownerClientId)
    {
        yield return null;

        NetworkObject playerInstace =
            Instantiate(playerPrefab, SpawnPoint.GetRandomSpawnPos(), Quaternion.identity);

        playerInstace.SpawnAsPlayerObject(ownerClientId);
    }
}
