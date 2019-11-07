using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private CellsController _cellsController;

    // Start is called before the first frame update
    void Start()
    {
        _cellsController = GetComponent<CellsController>();
        NewGame();
    }

    private void NewGame()
    {
        _cellsController.NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _cellsController.Move(KeyCode.UpArrow);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _cellsController.Move(KeyCode.DownArrow);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _cellsController.Move(KeyCode.RightArrow);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _cellsController.Move(KeyCode.LeftArrow);
        }
    }
}
