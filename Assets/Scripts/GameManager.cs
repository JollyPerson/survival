using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public Dictionary<int, GameObject> playerList = new Dictionary<int, GameObject>();
    public static GameManager instance;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
}