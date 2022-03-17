using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilsNS;

public class PlayerSelect : MonoBehaviour
{
    public static int numberPlayersInGame;

    public GameObject parentPortraitCanvas;
    public GameObject parentLandscapeCanvas;

    private Utils utils;

    private void Start()
	{
        utils = new Utils();
        utils.CheckIfOrientationUpdated(parentPortraitCanvas, parentLandscapeCanvas, true);
    }

	private void Update()
	{
        utils.CheckIfOrientationUpdated(parentPortraitCanvas, parentLandscapeCanvas, false);
    }
	public void ChoosePlayers(int numberOfPlayers)
    {
        numberPlayersInGame = numberOfPlayers;
    }
}
