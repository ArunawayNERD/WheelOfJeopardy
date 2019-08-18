using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Infobar : MonoBehaviour
{
    public TextMeshProUGUI round;
    public TextMeshProUGUI spinsLeft;

    public TextMeshProUGUI currentPlayer;

    public void updateRoundInfo(int currentRound, int spinsRemaining)
    {
        this.round.SetText(currentRound.ToString());
        this.spinsLeft.SetText(spinsRemaining.ToString());
    }

    public void updatePlayerName(string playerName)
    {
        this.currentPlayer.SetText(playerName);
    }
}
