using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerAiming : NetworkBehaviour

{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform turretTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        if (!IsOwner) { return; }

        Vector2 aimScreenPosition = inputReader.AimPosition;
        Vector2 aimWorldPosition = Camera.main.ScreenToWorldPoint(aimScreenPosition);

        turretTransform.up = new Vector2(
            aimWorldPosition.x - turretTransform.transform.position.x,
            aimWorldPosition.y - turretTransform.transform.position.y);
    }
}
