using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayoutScript : MonoBehaviour
{
    [SerializeField] private int maxColumnCount;
    [SerializeField] private Vector2 startingPos;
    [SerializeField] private int xOffset;
    [SerializeField] private int yOffset;

    public void UpdateLayout()
    {
        Debug.Log(startingPos.y + " " + yOffset);
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
