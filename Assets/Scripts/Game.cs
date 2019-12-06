using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private CellsController _cellsController;

    [SerializeField] Texture2D texture;
    private Sprite[] _sprites;
    

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _cellsController = GetComponent<CellsController>();

        _sprites = Resources.LoadAll<Sprite>(texture.name);
        
        yield return new WaitUntil(() => _sprites.Length == 14);

        NewGame(_sprites);
    }

    private void NewGame(Sprite[] _sprites)
    {
        _cellsController.NewGame(_sprites);
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
