using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckLayoutScript : MonoBehaviour
{
    [SerializeField] private int maxColumnCount;
    [SerializeField] private Vector2 startingPos;
    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;

    // Start is called before the first frame update
    void Start()
    {
        UpdateViewDeckLayout();
    }

    public void UpdateViewDeckLayout()
    {
        int columnNum = 0;
        int rowNum = 0;

        foreach (Transform child in transform)
        {
            child.localPosition = new Vector2(startingPos.x + xOffset * columnNum, startingPos.y - yOffset * rowNum);

            if (columnNum >= maxColumnCount-1)
            {
                columnNum = 0;
                rowNum++;
            }
            else
            {
                columnNum++;
            }
        }
    }

}
