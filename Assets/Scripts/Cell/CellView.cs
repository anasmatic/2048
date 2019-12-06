using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CellView : MonoBehaviour
{
    [SerializeField]Texture2D texture;
    private Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    internal void Init(Sprite[] _sprites)
    {
        //todo: move to singleton
        sprites = Resources.LoadAll<Sprite>(texture.name);
        if(sprites.Length == 14)//all sprites loaded
        {
            print("cool, all sprites loaded");
            //set default sprite as 0
            spriteRenderer.sprite = sprites[0];
        }
    }

    internal void MoveUpdate(CellModel cellData)
    {
        Jump(cellData);
        //Animate()
    }
    

    private void Jump(CellModel cellData)
    {
        print("Jump1:" + cellData);
        print("Jump2:" + cellData.value);
        print("Jump3:" + spriteRenderer);
        print("Jump4:" + spriteRenderer.sprite);
        transform.localPosition = (Vector2)cellData.pos;
        spriteRenderer.sprite = sprites[cellData.value];
    }
}
