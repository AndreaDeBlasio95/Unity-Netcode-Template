using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Rigidbody2D rb;

    [Space(10)]
    
    [Header("Settings")]
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 270f;
    [SerializeField] private Vector2 previousMovement;
    
    
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) { return; }
        inputReader.MoveEvent += HandleMove;
    }
    
    public override void OnNetworkDespawn()
    {
        if (!IsOwner) { return; }
        inputReader.MoveEvent -= HandleMove;
    }
    private void Update()
    {
        if (!IsOwner) { return; }
        float zRotation = previousMovement.x * -rotationSpeed * Time.deltaTime;
        bodyTransform.Rotate(0, 0, zRotation);
    }

    private void FixedUpdate()
    {
        if (!IsOwner) { return; }
        rb.velocity = movementSpeed * previousMovement.y * (Vector2)bodyTransform.up;
    }

    private void HandleMove (Vector2 movement){
        previousMovement = movement;
    }
}
