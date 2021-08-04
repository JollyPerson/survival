using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public float speed = 1;
    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * speed, moveVertical * speed, 0);
            transform.position = transform.position + movement;
        }
    }

    void Update()
    {
        HandleMovement();

        if (isLocalPlayer && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Sending Hola to server!");
            Hola();
        }
        
    }

    [TargetRpc]
    void ReplyHola()
    {
        Debug.Log("Hola from server");
    }
    
    [Command]
    void Hola()
    {
        Debug.Log("Received Hola from Client");
        ReplyHola();
    }
    
}
