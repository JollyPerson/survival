using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class MessageHandler : MonoBehaviour
{
    public Button button;
    public InputField field;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SedMessage()
    {
        Debug.Log("test");
        NetworkManager.INSTANCE.SendPacket(new Packet() { Data = new MessagePacketData(field.text), Type = PacketType.MESSAGE });
    }
}
