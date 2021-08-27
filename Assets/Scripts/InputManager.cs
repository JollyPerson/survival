using System.Collections;
using System.Collections.Generic;
using Survival_Game_Server.Packet.data;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public MovementType pressedKey;
    
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            pressedKey = MovementType.FORWARD;
            NetworkManager.INSTANCE.SendPacket(new Packet()
                {Data = new ClientInputPacketData(pressedKey), Type = PacketType.CLIENT_MOVEMENT_INPUT});
        }
    }
}
