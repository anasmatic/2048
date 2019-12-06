using UnityEngine;

public struct CellModel
{
    public Vector2Int pos;
    public int value;
    //movement
    public Vector2Int from;
    public Vector2Int to;
    //per move
    public bool willDestroy;
    public bool isNew;
}
