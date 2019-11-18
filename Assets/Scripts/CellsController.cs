using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsController : MonoBehaviour
{
    private CellsModel _cellsModel;
    private CellsView _cellsView;
    // Start is called before the first frame update
    void Start()
    {
        _cellsModel = GetComponent<CellsModel>();
        _cellsView = GetComponent<CellsView>();

        
    }

    internal void NewGame()
    {
        _cellsModel.Create4x4();

        FillEmptyCell();
        FillEmptyCell();
        FillEmptyCell();
        FillEmptyCell();
        //_cellsModel.DebugStart();
    }

    private void MoveUp()
    {
        _cellsModel.ShiftAllUp();
    }
    
    private void MoveDown()
    {
        _cellsModel.ShiftAllDown();
    }

    private void MoveRight()
    {
        _cellsModel.ShiftAllRight();
    }

    private void MoveLeft()
    {
        _cellsModel.ShiftAllLeft();
    }

    private Cell FillEmptyCell()
    {
        Cell cell = _cellsModel.FillEmptyCell();
        _cellsView.FillNewEmptyCell(FlipCoordinatesForView(cell));
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
        //TODO: notify view with merged cells 
        _cellsView.MergeCells(_cellsModel.MergedCells);
        //TODO: empty merged cells list here not in FillEmptyCell
        FillEmptyCell();
        //TODO: Notify view with new Empty Cell
        _cellsView.UpdateCells(_cellsModel.Cells4x4);
        //reset parameters as willDestroy and isNew , etc..
        _cellsModel.ResetCellsParameters();
    }

    private Cell FlipCoordinatesForView(Cell cell)
    {
        print("bfr:" + cell);
        cell.pos.x = cell.pos.x + cell.pos.y;
        cell.pos.y = cell.pos.x - cell.pos.y;
        cell.pos.x = cell.pos.x - cell.pos.y;
        cell.pos.y *= -1;
        print("ftr:" + cell.pos);
        return cell;
    }
}
