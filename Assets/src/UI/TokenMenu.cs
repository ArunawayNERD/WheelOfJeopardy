using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TokenMenu : MonoBehaviour
{
    public GameEngine gameEngine;
    public GameObject menuGraphics;

    // Start is called before the first frame update
    void Awake()
    {
        this.UpdateVisability(false);
    }

	public void UpdateVisability(bool show)
	{
		this.menuGraphics.SetActive(show);
    }

    public void HandleYesNoClicked(bool useToken)
    {
        gameEngine.handleTokenDecision(useToken);
        this.UpdateVisability(false);
    }
}
