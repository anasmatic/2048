using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellsModel : MonoBehaviour
{
    [SerializeField] private Text _debugText;
    [SerializeField] private bool _useTextsArray;
    [SerializeField] private Text[] _debugTexts;
    private int _currentDebugTextIndex;

    [System.Obsolete]private int[,] cellsInt4x4 = new int[4,4];
    private CellModel[,] cells4x4 = new CellModel[4, 4];
    public CellModel[,] Cells4x4 { get => cells4x4; set => cells4x4 = value; }

    private List<Vector2Int> _mergedCellsInt;
    public List<Vector2Int> MergedCellsInt { get => _mergedCellsInt; }

    private List<CellModel> _mergedCells;
    public List<CellModel> MergedCells { get => _mergedCells; }

    private Vector2Int _lastCell;

    // map cell all-cells-controllers multiDim array to cellsModel array of arrays
    public void Create4x4(ref CellController[] cellControllers)
    {
        int index = 0;
        //cellsInt4x4 = new int[4,4];
        cells4x4 = new CellModel[4, 4];
        for (int i = 0; i < cells4x4.GetLength(0); i++)
        {
            for (int j = 0; j < cells4x4.GetLength(1); j++)
            {
                //add cell model from cell controllers to cells Array
                index = (i * 4) + j;
                cellControllers[index].cellModel.pos =
                    cellControllers[index].cellModel.from =
                        cellControllers[index].cellModel.to = new Vector2Int(i, j);
                cells4x4[i, j] = cellControllers[index].cellModel;
            }
        }

        _mergedCellsInt = new List<Vector2Int>();
        _mergedCells = new List<CellModel>();
    }

    // Update is called once per frame
    internal CellModel FillEmptyCell()
    {
        CellModel cell = GetEmptyCell();
        cell.from = cell.to = cell.pos;
        cell.willDestroy = false;
        cell.isNew = true;
        cell.value = 
            //cellsInt4x4[cell.pos.x, cell.pos.y] = 
            Random.Range(1, 2);
        cells4x4[cell.pos.x, cell.pos.y] = cell;
        DrawDebugArrayCells(cell, "yellow");
        //DrawDebugArray(cell.pos, "yellow");
        return cell;
    }

    internal void ResetCellsParameters()
    {
        for (int i = 0; i < cells4x4.GetLength(0); i++)
        {
            for (int j = 0; j < cells4x4.GetLength(1); j++)
            {
                cells4x4[i, j].from = cells4x4[i, j].to = cells4x4[i, j].pos;
                cells4x4[i, j].willDestroy = false;
                cells4x4[i, j].isNew = false;
            }
        }
    }

    internal void ClearMergedCellsList()
    {
        _mergedCellsInt.Clear();
    }

    [System.Obsolete]private void DrawDebugArray(Vector2Int cell, string color = "white")
    {
        string debug = "";
        Text currentText;
        if (_useTextsArray)
        {
            if (_currentDebugTextIndex == _debugTexts.Length)
            {
                _currentDebugTextIndex = 0;
                Debug.Break();
            }

            currentText = _debugTexts[_currentDebugTextIndex];
            _currentDebugTextIndex++;
        }
        else
        {
            currentText = _debugText;
        }

        for (int i = 0; i < cellsInt4x4.GetLength(0); i++)
        {
            debug += "\n{";
            for (int j = 0; j < cellsInt4x4.GetLength(1); j++)
            {
                if(cell.x == i && cell.y == j)
                    debug += " <color="+ color + ">" + cellsInt4x4[i, j] + "</color>, ";
                else
                    debug += " " + cellsInt4x4[i, j] + ", ";
            }
            debug += "},\n";
        }
        
        currentText.text = debug;
    
    }

    private void DrawDebugArrayCells(CellModel cell, string color = "white")
    {
        string debug = "";
        Text currentText;
        if (_useTextsArray)
        {
            if (_currentDebugTextIndex == _debugTexts.Length)
            {
                _currentDebugTextIndex = 0;
                Debug.Break();
            }

            currentText = _debugTexts[_currentDebugTextIndex];
            _currentDebugTextIndex++;
        }
        else
        {
            currentText = _debugText;
        }

        for (int i = 0; i < cells4x4.GetLength(0); i++)
        {
            debug += "\n";
            for (int j = 0; j < cells4x4.GetLength(1); j++)
            {
                if (cell.pos.x == i && cell.pos.y == j)
                    debug += " <color=" + color + ">" + cells4x4[i, j].value + "</color>, ";
                    //debug += "|   <color=" + color + ">" + cells4x4[i, j].value + "</color>    |, ";
                else
                    debug += " " + cells4x4[i, j].value + " , ";
                    //debug += "|   " + cells4x4[i, j].value + "    |, ";
            }
            /*debug += "\n";
            for (int j = 0; j < cells4x4.GetLength(1); j++)
            {
                if (cell.pos.x == i && cell.pos.y == j)
                    debug += "| <color=" + color + ">" + cells4x4[i, j].from + "</color>|, ";
                else
                    debug += "| " + cells4x4[i, j].from + "|, ";
            }
            debug += "\n";
            for (int j = 0; j < cells4x4.GetLength(1); j++)
            {
                if (cell.pos.x == i && cell.pos.y == j)
                    debug += "| <color=" + color + ">" + cells4x4[i, j].to + "</color>|, ";
                else
                    debug += "| " + cells4x4[i, j].to + "|, ";
            }
            debug += "\n";
            for (int j = 0; j < cells4x4.GetLength(1); j++)
            {
                if (cell.pos.x == i && cell.pos.y == j)
                    debug += "| <color=" + color + ">" + cells4x4[i, j].willDestroy + "</color>|, ";
                else
                    debug += "| " + cells4x4[i, j].willDestroy + "|, ";
            }*/
            debug += ",\n";
        }
        currentText.text = debug;
    }

    System.Random rnd = new System.Random();
    internal CellModel GetEmptyCell()
    {
        //int row = rnd.Next(cellsInt4x4.GetLength(0));
        //int column = rnd.Next(cellsInt4x4.GetLength(1));
        int row = rnd.Next(cells4x4.GetLength(0));
        int column = rnd.Next(cells4x4.GetLength(1));

        //if (cellsInt4x4[row, column] == 0)
        if (cells4x4[row, column].value == 0)
            return cells4x4[row, column];
        else
            return GetEmptyCell();
    }
    private void MergeTwoCells(ref CellModel cell, ref CellModel lastCell)
    {
        lastCell.value += 1;
        lastCell.to = cell.pos;
        lastCell.willDestroy = false;
        _mergedCells.Add(lastCell);

        cell.value = 0;
        cell.willDestroy = true;
        cell.from = cell.to = cell.pos;
    }
    private void SwapTwoCells(ref CellModel cell, ref CellModel lastCell)
    {
        lastCell.value = cell.value;
        lastCell.to = cell.pos;
        lastCell.willDestroy = false;

        cell.value = 0;
        cell.willDestroy = true;
        cell.from = cell.to = cell.pos;
    }

    [System.Obsolete]private void MergeTwoCells(Vector2Int cell , Vector2Int lastCell)
    {
        cellsInt4x4[(int)lastCell.x, (int)lastCell.y] += 1;
        _mergedCellsInt.Add(lastCell);

        cellsInt4x4[(int)cell.x, (int)cell.y] = 0;
    }

    [System.Obsolete]private void SwapTwoCells(Vector2Int cell , Vector2Int lastCell)
    {
        cellsInt4x4[(int)lastCell.x, (int)lastCell.y] = cellsInt4x4[(int)cell.x, (int)cell.y];
        cellsInt4x4[(int)cell.x, (int)cell.y] = 0;
    }
         

    //------------------------- Up -----------------------
    public void ShiftAllUp()
    {
        //start from top (0,0)
        for (int i = 0; i < cells4x4.GetLength(0); i++)
        {
            if (i > 0)
            {
                for (int j = 0; j < cells4x4.GetLength(1); j++)
                {
                    MoveCellUp(i, j);
                }
            }
        }
    }

    private void MoveCellUp(int x, int y)
    {
        //if we haven't reached first row 
        if ((x - 1) >= 0)
        {
            //if the cell above is empty
            if (cells4x4[ x-1 , y ].value == 0)
            {
                SwapTwoCells(ref cells4x4[x , y], ref cells4x4[ x-1 , y]);
                MoveCellUp( x-1 , y);
            }
            //if the cell above was not merged before && the cell above is as same as current
            else if (_mergedCells.Contains(cells4x4[x - 1, y]) == false &&
                cells4x4[ x-1 , y ].value == cells4x4[ x , y ].value)
            {
                MergeTwoCells(ref cells4x4[x, y], ref cells4x4[ x-1 , y]);
                MoveCellUp( x-1 , y );
            }
        }
    }

    [System.Obsolete]public void MoveCellUp(Vector2Int cell)
    {
        //if we haven't reached first row 
        if ((cell.x-1) >= 0 )
        {
            _lastCell.x = cell.x - 1;
            _lastCell.y = cell.y;
            //if the cell above is empty
            if (cellsInt4x4[(int)_lastCell.x, (int)_lastCell.y] == 0)
            {
                SwapTwoCells(cell,_lastCell);
                MoveCellUp(_lastCell);
            }
            //if the cell above was not merged before && the cell above is as same as current
            else if (_mergedCellsInt.Contains(_lastCell) == false &&
                cellsInt4x4[(int)_lastCell.x, (int)_lastCell.y] == cellsInt4x4[(int)cell.x, (int)cell.y])
            {
                MergeTwoCells(cell,_lastCell);
                MoveCellUp(_lastCell);
            }
            
        }
    }

    //------------------------- Down -----------------------
    public void ShiftAllDown()
    {
        //start from bottom (4,0)
        for (int i = cells4x4.GetLength(0)-1; i >= 0; i--)
        {
            if (i < cells4x4.GetLength(0)-1)
            {
                for (int j = 0; j < cells4x4.GetLength(1); j++)
                {
                    MoveCellDown(i, j);
                }
            }
        }
    }
    private void MoveCellDown(int x, int y)
    {
        //if we haven't reached last row 
        if ((x + 1) < cells4x4.GetLength(0))
        {
            //if the cell below is empty
            if (cells4x4[ x+1 , y].value == 0)
            {
                SwapTwoCells(ref cells4x4[x , y], ref cells4x4[x + 1, y]);
                MoveCellDown( x+1 , y );
            }
            //
            else if (_mergedCells.Contains(cells4x4[x + 1, y]) == false &&
                cells4x4[ x+1 , y].value == cells4x4[ x, y ].value)
            {
                MergeTwoCells(ref cells4x4[x , y], ref cells4x4[ x+1 , y]);
                MoveCellDown( x+1 , y);
            }
        }
    }
    [System.Obsolete]private void MoveCellDown(Vector2Int cell)
    {
        //if we haven't reached last row 
        if ((cell.x+1) < cellsInt4x4.GetLength(0))
        {
            _lastCell.x = cell.x + 1;
            _lastCell.y = cell.y;
            //if the cell below is empty
            if (cellsInt4x4[(int)_lastCell.x, (int)_lastCell.y] == 0)
            {
                SwapTwoCells(cell,_lastCell);
                MoveCellDown(_lastCell);
            }
            //
            else if (_mergedCellsInt.Contains(_lastCell) == false && 
                cellsInt4x4[(int)_lastCell.x, (int)_lastCell.y] == cellsInt4x4[(int)cell.x, (int)cell.y])
            {
                MergeTwoCells(cell,_lastCell);
                MoveCellDown(_lastCell);
            }
        }
    }

    //------------------------- Left -----------------------
    public void ShiftAllLeft()
    {
        //start from left (0,0)
        for (int j = 0; j < cells4x4.GetLength(1); j++)
        {
            if (j > 0)
            {
                //loop rows
                for (int i = 0; i < cells4x4.GetLength(0); i++)
                {
                    MoveCellLeft(i, j);
                }
            }
        }
    }
    private void MoveCellLeft(int x, int y)
    {
        //if we haven't reached last row -far left-
        if (y - 1 >= 0)
        {
            //if cell on the left is empty
            if (cells4x4[x, y-1].value == 0)
            {
                SwapTwoCells(ref cells4x4[x, y], ref cells4x4[ x, y-1]);
                MoveCellLeft(x, y-1);
            }
            //last cell on left was not merged && it equals the cell next to it
            else if (_mergedCells.Contains(cells4x4[x, y - 1]) == false &&
                cells4x4[x, y-1].value == cells4x4[x, y].value)
            {
                MergeTwoCells(ref cells4x4[x, y], ref cells4x4[x, y-1]);
                MoveCellLeft(x, y-1);
            }
        }
    }
    [System.Obsolete]private void MoveCellLeft(Vector2Int cell)
    {
        //if we haven't reached last row -far left-
        if (cell.y - 1 >= 0)
        {
            _lastCell.x = cell.x;
            _lastCell.y = cell.y-1;
            //if cell on the left is empty
            if (cellsInt4x4[(int) _lastCell.x, (int) _lastCell.y] == 0)
            {
                SwapTwoCells(cell,_lastCell);
                MoveCellLeft(_lastCell);
            }
            //last cell on left was not merged && it equals the cell next to it
            else if (_mergedCellsInt.Contains(_lastCell) == false &&
                cellsInt4x4[(int) _lastCell.x, (int) _lastCell.y] == cellsInt4x4[(int)cell.x, (int)cell.y])
            {
                MergeTwoCells(cell,_lastCell);
                MoveCellLeft(_lastCell);
            }
        }
    }

    //------------------------- Right -----------------------
    public void ShiftAllRight()
    {
        //start from left (0,0)
        for (int j = cells4x4.GetLength(1)-1; j>=0; j--)
        {
            if (j < cells4x4.GetLength(1)-1)
            {
                //loop rows
                for (int i = 0; i < cells4x4.GetLength(0); i++)
                {
                    MoveCellRight(i, j);
                }
            }
        }
    }

    private void MoveCellRight(int x, int y)
    {
        //if we haven't reached last row -far right-
        if (y+1 < cells4x4.GetLength(1))
        {
            //if cell on the right is empty
            if (cells4x4[x, y+1].value == 0)
            {
                SwapTwoCells(ref cells4x4[x, y], ref cells4x4[x, y+1 ]);
                MoveCellRight(x, y+1);
            }
            else if(_mergedCells.Contains(cells4x4[x, y + 1]) ==false && 
                cells4x4[x, y+1 ].value == cells4x4[x, y].value)
            {
                MergeTwoCells(ref cells4x4[x, y], ref cells4x4[x, y+1]);
                MoveCellRight(x, y+1);
            }
        }
    }
    [System.Obsolete]private void MoveCellRight(Vector2Int cell)
    {
        //if we haven't reached last row -far right-
        if (cell.y + 1 < cellsInt4x4.GetLength(1))
        {
            _lastCell.x = cell.x;
            _lastCell.y = cell.y + 1;
            //if cell on the right is empty
            if (cellsInt4x4[(int)_lastCell.x, (int)_lastCell.y] == 0)
            {
                SwapTwoCells(cell, _lastCell);
                MoveCellRight(_lastCell);
            }
            else if (_mergedCellsInt.Contains(_lastCell) == false &&
                cellsInt4x4[(int)_lastCell.x, (int)_lastCell.y] == cellsInt4x4[(int)cell.x, (int)cell.y])
            {
                MergeTwoCells(cell, _lastCell);
                MoveCellRight(_lastCell);
            }
        }
    }
}