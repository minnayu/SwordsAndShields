using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileManager tileManager;
    public TileManager.Owner owner;

    public SpriteRenderer spriteRenderer;
    public Sprite sword; // Drag your first sprite here
    public Sprite shield; // Drag your second sprite here
    public Sprite square;
    private void OnMouseUp()
    {
        // Check for current player  that is claiming ownership of this space
        owner = tileManager.CurrentPlayer;
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Set the sprite color to represent the owner (Sword = Blue, Shield = Red)
        if (owner == TileManager.Owner.Sword)
            spriteRenderer.sprite = sword;
        else if (owner == TileManager.Owner.Shield)
            spriteRenderer.sprite = shield;

        // Update to the next player in rotation
        tileManager.ChangePlayer();
    }

    public void ResetTile()
    {
        spriteRenderer.sprite = square;
        owner = tileManager.CurrentPlayer;
    }
}
