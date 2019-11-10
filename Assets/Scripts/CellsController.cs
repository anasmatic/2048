using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsController : MonoBehaviour
{
    private CellsModel _cellsModel;
    // Start is called before the first frame update
    void Start()
    {
        _cellsModel = GetComponent<CellsModel>();
    }

    internal void NewGame()
    {
        //FillEmptyCell();
        //FillEmptyCell();
        _cellsModel.Create4x4();
        _cellsModel.DebugStart();
    }

    public void MoveUp()
    {
        _cellsModel.ShiftAllUp();
    }
    
    public void MoveDown()
    {
        _cellsModel.ShiftAllDown();
    }

    public void MoveRight()
    {
        _cellsModel.ShiftAllRight();
    }

    public void MoveLeft()
    {
        _cellsModel.ShiftAllLeft();
    }

    private Vector2 FillEmptyCell()
    {
        Vector2 cell = _cellsModel.FillEmptyCell();
        print(cell.x +","+ cell.y);
        return cell;
    }

    public void Move(KeyCode direction)
    {
        switch (direction)
        {
            case KeyCode.UpArrow:
                MoveUp();
                break;
            case KeyCode.DownArrow:
                MoveDown();
                break;
            case KeyCode.RightArrow:
                MoveRight();
                break;
            case KeyCode.LeftArrow:
                MoveLeft();
                break;
        }

        FillEmptyCell();
    }
}
