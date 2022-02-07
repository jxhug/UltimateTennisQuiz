using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{

    public static int numberPlayersInGame;
	public void ChoosePlayers(int numberOfPlayers)
    {
        numberPlayersInGame = numberOfPlayers;
    }
}
