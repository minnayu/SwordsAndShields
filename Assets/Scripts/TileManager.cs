using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public Owner CurrentPlayer;
    public Tile[] Tiles = new Tile[9];

    private Text swordText;
    private Text shieldText;

    public GameObject ResetButton;
    public GameObject QuitButton;

    public int turnCount;
    
    public enum Owner
    {
        None,
        Sword,
        Shield
    }

    private bool won;

    // Start is called before the first frame update
    void Start()
    {
        won = false;
        CurrentPlayer = Owner.Sword;
        swordText = GameObject.Find("SwordScore").GetComponent<Text>();
        shieldText = GameObject.Find("ShieldScore").GetComponent<Text>();
    }

    public void ChangePlayer()
    {
        if (CheckForWinner())
            return;

        if (CurrentPlayer == Owner.Sword)
            CurrentPlayer = Owner.Shield;
        else
            CurrentPlayer = Owner.Sword;
    }

    public bool CheckForWinner()
    {
        //top row
        if (Tiles[0].owner == CurrentPlayer && Tiles[1].owner == CurrentPlayer && Tiles[3].owner == CurrentPlayer)
            won = true;
        //middle row
        else if (Tiles[3].owner == CurrentPlayer && Tiles[4].owner == CurrentPlayer && Tiles[5].owner == CurrentPlayer)
            won = true;
        //bottom row
        else if (Tiles[6].owner == CurrentPlayer && Tiles[7].owner == CurrentPlayer && Tiles[8].owner == CurrentPlayer)
            won = true;
        //left column
        else if (Tiles[0].owner == CurrentPlayer && Tiles[3].owner == CurrentPlayer && Tiles[6].owner == CurrentPlayer)
            won = true;
        //middle column
        else if (Tiles[1].owner == CurrentPlayer && Tiles[4].owner == CurrentPlayer && Tiles[7].owner == CurrentPlayer)
            won = true;
        //right column
        else if (Tiles[2].owner == CurrentPlayer && Tiles[5].owner == CurrentPlayer && Tiles[8].owner == CurrentPlayer)
            won = true;
        //diagonal left
        else if (Tiles[0].owner == CurrentPlayer && Tiles[4].owner == CurrentPlayer && Tiles[8].owner == CurrentPlayer)
            won = true;
        //diagonal right
        else if (Tiles[2].owner == CurrentPlayer && Tiles[4].owner == CurrentPlayer && Tiles[6].owner == CurrentPlayer)
            won = true;

        if (won)
        {
            Debug.Log("Winner: " + CurrentPlayer);
            AddToScore();
            RevealResetButton();
            RevealQuitButton();
            CurrentPlayer = Owner.None;
            return true;
        }
           
        turnCount++;
        if (turnCount == 9)
        {
            RevealQuitButton();
            RevealResetButton();
            CurrentPlayer = Owner.None;
        }

        Debug.Log(turnCount);
        return false;
    }

    public void AddToScore()
    {
        if(CurrentPlayer == Owner.Sword)
        {
            swordText.GetComponent<ScoreController>().swordScore += 1;
            swordText.GetComponent<ScoreController>().UpdateSwordScore();
        }
        else if(CurrentPlayer == Owner.Shield)
        {
            shieldText.GetComponent<ScoreController>().shieldScore += 1;
            shieldText.GetComponent<ScoreController>().UpdateShieldScore();
        }
    }

    public void RevealResetButton()
    {
        ResetButton.SetActive(true);
    }
    public void ResetBoard()
    {
        for(int i=0; i < Tiles.Length; i++)
        {
            
            Tiles[i].ResetTile();
            Debug.Log("Reset" + i);
        }
        won = false;
        CurrentPlayer = Owner.Sword;
        ResetButton.SetActive(false);
        QuitButton.SetActive(false);
        turnCount = 0;
    }

    public void RevealQuitButton()
    {
        QuitButton.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Application");
    }
}
