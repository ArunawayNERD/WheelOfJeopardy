using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.src.PlayerScoring;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI playerOneRoundOneScore;
    public TextMeshProUGUI playerOneRoundTwoScore;
    public TextMeshProUGUI playerOneTokens;

    public TextMeshProUGUI playerTwoRoundOneScore;
    public TextMeshProUGUI playerTwoRoundTwoScore;
    public TextMeshProUGUI playerTwoTokens;

    public TextMeshProUGUI playerThreeRoundOneScore;
    public TextMeshProUGUI playerThreeRoundTwoScore;
    public TextMeshProUGUI playerThreeTokens;

    internal void updatePlayerInfo(Player playerOne, Player playerTwo, Player playerThree)
    {
        this.playerOneRoundOneScore.SetText(playerOne.RoundOneScore.ToString());
        this.playerOneRoundTwoScore.SetText(playerOne.RoundTwoScore.ToString());
        this.playerOneTokens.SetText(playerOne.Tokens.ToString());

        this.playerTwoRoundOneScore.SetText(playerTwo.RoundOneScore.ToString());
        this.playerTwoRoundTwoScore.SetText(playerTwo.RoundTwoScore.ToString());
        this.playerTwoTokens.SetText(playerTwo.Tokens.ToString());

        this.playerThreeRoundOneScore.SetText(playerThree.RoundOneScore.ToString());
        this.playerThreeRoundTwoScore.SetText(playerThree.RoundTwoScore.ToString());
        this.playerThreeTokens.SetText(playerThree.Tokens.ToString());
    }
}
