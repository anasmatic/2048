using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsController : MonoBehaviour
{
    private CellsModel _cellsModel;
    private CellsView _cellsView;
    [SerializeField]private GameObject gridContainer;
    [SerializeField] private GameObject cellPrefab;
    private CellController[] allCells = new CellController[4*4];
    
    // Start is called before the first frame update
    void Start()
    {
        _cellsModel = GetComponent<CellsModel>();
        _cellsView = GetComponent<CellsView>();
        
    }

    internal void NewGame(Sprite[] _sprites)
    {
        //init each cell controller
        allCells = new CellController[4 * 4];
        for (int i = 0; i < allCells.Length; i++)
        {
            print(i+":"+ allCells[i]);
            allCells[i] = Instantiate(cellPrefab, gridContainer.transform, true).GetComponent<CellController>();
            allCells[i].Init(_sprites);
        }
        _cellsModel.Create4x4(ref allCells);

        FillEmptyCell();
        FillEmptyCell();
        FillEmptyCell();
        FillEmptyCell();

        Move(KeyCode.None);
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

    private CellModel FillEmptyCell()
    {
        CellModel cell = _cellsModel.FillEmptyCell();
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
        //_cellsView.MergeCells(_cellsModel.MergedCells);
        //TODO: empty merged cells list here not in FillEmptyCell
        //FillEmptyCell();
        //update Every CellController
        for (int i = 0; i < allCells.Length; i++)
        {
            allCells[i].MoveUpdate();
        }
        //TODO: Notify view with new Empty Cell
        //_cellsView.UpdateCells(_cellsModel.Cells4x4);
        //reset parameters as willDestroy and isNew , etc..
        _cellsModel.ResetCellsParameters();
    }

    private CellModel FlipCoordinatesForView(CellModel cell)
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
