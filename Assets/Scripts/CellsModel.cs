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
    // Start is called before the first frame update
    public void Create4x4()
    {
        cells4x4 = new int[4,4];
    }

    // Update is called once per frame
    internal Vector2 FillEmptyCell()
    {
        Vector2 cell = GetEmptyCell();
        cells4x4[(int) cell.x, (int) cell.y] = Random.Range(1, 2)*2;

        DrawDebugArray(cell);
        return cell;
    }

    private void DrawDebugArray(Vector2 cell)
    {
        string debug = "";
        Text currentText;
        if (_useTextsArray)
        {
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
                    debug += " <color=color>" + cells4x4[i, j] + "</color>, ";
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

    public void MoveCellUp(Vector2 cell)
    {
        //if we haven't reached first row 
        if ((cell.x-1) >= 0 )
        {
            //if the cell above is empty
            if (cells4x4[(int) cell.x-1, (int) cell.y] == 0)
            {
                cells4x4[(int) cell.x - 1, (int) cell.y] = cells4x4[(int) cell.x, (int) cell.y];
                cells4x4[(int) cell.x, (int) cell.y] = 0;
                cell.x -= 1;
                MoveCellUp(cell);
            }
        }
    }

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
            //if the cell below is empty
            if (cells4x4[(int)cell.x + 1, (int)cell.y] == 0)
            {
                cells4x4[(int)cell.x + 1, (int)cell.y] = cells4x4[(int)cell.x, (int)cell.y];
                cells4x4[(int)cell.x, (int)cell.y] = 0;
                cell.x += 1;
                MoveCellDown(cell);
            }
        }
    }

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
}
