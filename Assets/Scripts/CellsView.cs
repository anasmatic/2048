using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsView : MonoBehaviour
{
    [SerializeField] private GameObject _gridContainer;
    [SerializeField] private List<ViewDataCell> _cellsMapper;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void MergeCells(List<Cell> mergedCells)
    {
        
    }

    internal void FillNewEmptyCell(Cell cell)
    {
        print(cell.pos);
        
        GameObject newCell = Instantiate(SelectPrefabAccordingToCellValue(cell.value), 
                                            _gridContainer.transform, true);
        newCell.transform.localPosition = (Vector2) cell.pos;
        //_gridContainer
    }

    private GameObject SelectPrefabAccordingToCellValue(int value)
    {
        return _cellsMapper[value]._cellPrefab;
    }

    internal void UpdateCells(Cell[,] cells4x4)
    {
        
    }
}
/*
 0 , 1 , 2 , 3 , 4 , 5 , 6 , 7 , 8 , 9 , 10 , 11 , 12 , 13
 0,  2 , 4 , 8 , 16, 32, 64,128,256,512,1024,2084,4096,8192

 * */
