using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerScript : MonoBehaviour
{
    public int playerId;
    private Player player;

    private void Awake()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    private void Update()
    {
        
    }
}
