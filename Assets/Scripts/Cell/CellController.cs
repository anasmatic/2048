using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellController : MonoBehaviour
{
    internal CellModel cellModel;
    internal CellView cellView;

    // Start is called before the first frame update
    internal void Init(Sprite[] _sprites)
    {
        //cellModel.pos will be assigned later in CellsModel
        cellModel.from = cellModel.to = cellModel.pos;
        cellModel.willDestroy = false;
        cellModel.isNew = false;
        cellModel.value = 0;

        cellView = GetComponent<CellView>();
        cellView.Init(_sprites);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void MoveUpdate()
    {
        cellView.MoveUpdate(cellModel);
    }

}
