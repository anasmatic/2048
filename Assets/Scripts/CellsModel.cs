using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellsModel : MonoBehaviour
{
    [SerializeField] private Text _debugText;
    [SerializeField] private bool _useTextsArray;
    [SerializeField] private Text[] _debugTexts;
    private int _currentDebugTextIndex;

    private int[,] cells4x4 = new int[4,4];

    private List<Vector2> _mergedCells;
    // Start is called before the first frame update
    public void Create4x4()
    {
        cells4x4 = new int[4,4];
        _mergedCells = new List<Vector2>();
    }
    public void DebugStart()
    {
        for (int i = 0; i < _debugTexts.Length; i++)
        {
            _debugTexts[i].text = "---" + (i + 1) + "---";
        }
        cells4x4 = new int[4, 4]
        {
            {0, 2, 0, 0},
            {0, 2, 0, 0},
            {0, 2, 0, 0},
            {0, 2, 0, 0}
        };
        DrawDebugArray(Vector2.zero);
    }

    // Update is called once per frame
    internal Vector2 FillEmptyCell()
    {
        Vector2 cell = GetEmptyCell();
        cells4x4[(int) cell.x, (int) cell.y] = Random.Range(1, 2)*2;

        DrawDebugArray(cell,"yellow");
        //when we introduce a new cell this means merging and moving process is done
        _mergedCells.Clear();
        return cell;
    }

    private void DrawDebugArray(Vector2 cell, string color = "white")
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
            debug += "\n{";
            for (int j = 0; j < cells4x4.GetLength(1); j++)
            {
                if(cell.x == i && cell.y == j)
                    debug += " <color="+ color + ">" + cells4x4[i, j] + "</color>, ";
                else
                    debug += " " + cells4x4[i, j] + ", ";
            }
            debug += "},\n";
        }
        
        currentText.text = debug;
    
    }

    internal Vector2 GetEmptyCell()
    {
        System.Random rnd = new System.Random();
        int row = rnd.Next(cells4x4.GetLength(0));
        int column = rnd.Next(cells4x4.GetLength(1));

        if (cells4x4[row, column] == 0)
            return new Vector2(row, column);
        else
            return GetEmptyCell();
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
                    MoveCellUp(new Vector2(i, j));
                }
            }
        }
    }

    private Vector2 _lastCell;
    public void MoveCellUp(Vector2 cell)
    {
        //if we haven't reached first row 
        if ((cell.x-1) >= 0 )
        {
            _lastCell.x = cell.x - 1;
            _lastCell.y = cell.y;
            //if the cell above is empty
            if (cells4x4[(int)_lastCell.x, (int)_lastCell.y] == 0)
            {
                cells4x4[(int)_lastCell.x, (int)_lastCell.y] = cells4x4[(int) cell.x, (int) cell.y];
                cells4x4[(int) cell.x, (int) cell.y] = 0;
                //cell.x = _lastCell.x;

                MoveCellUp(_lastCell);
            }
            //if the cell above was not merged before && the cell above is as same as current
            else if (_mergedCells.Contains(_lastCell) == false &&
                cells4x4[(int)_lastCell.x, (int)_lastCell.y] == cells4x4[(int)cell.x, (int)cell.y])
            {
                cells4x4[(int)_lastCell.x, (int)_lastCell.y] *= cells4x4[(int)cell.x, (int)cell.y];
                cells4x4[(int)cell.x, (int)cell.y] = 0;
                //cell.x = _lastCell.x;

                _mergedCells.Add(_lastCell);
                DrawDebugArray(_lastCell);
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
                    MoveCellDown(new Vector2(i, j));
                }
            }
        }
    }

    private void MoveCellDown(Vector2 cell)
    {
        //if we haven't reached last row 
        if ((cell.x+1) < cells4x4.GetLength(0))
        {
            _lastCell.x = cell.x + 1;
            _lastCell.y = cell.y;
            //if the cell below is empty
            if (cells4x4[(int)_lastCell.x, (int)_lastCell.y] == 0)
            {
                cells4x4[(int)_lastCell.x, (int)_lastCell.y] = cells4x4[(int)cell.x, (int)cell.y];
                cells4x4[(int)cell.x, (int)cell.y] = 0;
                //cell.x = 1;
                MoveCellDown(_lastCell);
            }
            //
            else if (_mergedCells.Contains(_lastCell) == false && 
                cells4x4[(int)_lastCell.x, (int)_lastCell.y] == cells4x4[(int)cell.x, (int)cell.y])
            {
                cells4x4[(int)_lastCell.x, (int)_lastCell.y] *= cells4x4[(int)cell.x, (int)cell.y];
                cells4x4[(int)cell.x, (int)cell.y] = 0;
                //cell.x += 1;

                _mergedCells.Add(_lastCell);
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
                    MoveCellLeft(new Vector2(i, j));
                }
            }
        }
    }

    private void MoveCellLeft(Vector2 cell)
    {
        //if we haven't reached last row -far left-
        if (cell.y - 1 >= 0)
        {
            if (cells4x4[(int) cell.x, (int) cell.y - 1] == 0)
            {
                cells4x4[(int)cell.x, (int)cell.y-1] = cells4x4[(int)cell.x, (int)cell.y];
                cells4x4[(int)cell.x, (int)cell.y] = 0;
                cell.y -= 1;
                MoveCellLeft(cell);
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
                    MoveCellRight(new Vector2(i, j));
                }
            }
        }
    }

    private void MoveCellRight(Vector2 cell)
    {
        //if we haven't reached last row -far right-
        if (cell.y+1 < cells4x4.GetLength(1))
        {
            if (cells4x4[(int)cell.x, (int)cell.y + 1] == 0)
            {
                cells4x4[(int)cell.x, (int)cell.y + 1] = cells4x4[(int)cell.x, (int)cell.y];
                cells4x4[(int)cell.x, (int)cell.y] = 0;
                cell.y += 1;
                MoveCellRight(cell);
            }
        }
    }
}
