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

    internal void MergeCells(List<CellModel> mergedCells)
    {
        
    }

    internal void FillNewEmptyCell(CellModel cell)
    {
        print("old FillNewEmptyCell ->" + cell.pos);
        /*
        GameObject newCell = Instantiate(SelectPrefabAccordingToCellValue(cell.value), 
                                            _gridContainer.transform, true);
        newCell.transform.localPosition = (Vector2) cell.pos;
        //_gridContainer
        */
    }

    private GameObject SelectPrefabAccordingToCellValue(int value)
    {
        return _cellsMapper[value]._cellPrefab;
    }

    internal void UpdateCells(CellModel[,] cells4x4)
    {
        for (int i = 0; i < cells4x4.GetLength(0); i++)
        {
            for (int j = 0; j < cells4x4.GetLength(1); j++)
            {
                if(cells4x4[i, j].value == 0)
                    cells4x4[i, j].to = cells4x4[i, j].pos;
                cells4x4[i, j].willDestroy = false;
                cells4x4[i, j].isNew = false;
            }
        }
    }
}
/*
 0 , 1 , 2 , 3 , 4 , 5 , 6 , 7 , 8 , 9 , 10 , 11 , 12 , 13
 0,  2 , 4 , 8 , 16, 32, 64,128,256,512,1024,2084,4096,8192

 * */
