using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridResizer : GridLayoutGroup
{
    public Vector2Int gridLength = Vector2Int.one;
    public bool isSquared;

    RectTransform ui_RT;

    bool isHorz;


    protected override void OnEnable()
    {
        ui_RT = GetComponent<RectTransform>();
        useGUILayout = false;
    }

    protected override void Start()
    {
        base.Start();
        ResizeCells();
    }

    //private void OnGUI()
    //{
    //    ResizeCells();
    //}

    Vector2 GetGridSize()
    {
        //Substract padding
        float horz = ui_RT.rect.width - (padding.left + padding.right);
        float vert = ui_RT.rect.height - (padding.top + padding.bottom);

        //Check orientation
        isHorz = horz >= vert;

        //Substract spacing
        float horzSpacing =
            isHorz ?
            spacing.x * (gridLength.x - 1) :
            spacing.x * (gridLength.y - 1);
        float vertSpacing =
            isHorz ?
            spacing.y * (gridLength.y - 1) :
            spacing.y * (gridLength.x - 1);

        return new Vector2(horz - horzSpacing, vert - vertSpacing);
    }

    void ResizeCells()
    {
        Vector2 gridSize = GetGridSize();

        float horz =
            isHorz ?
            gridSize.x / gridLength.x :
            gridSize.x / gridLength.y;

        float vert =
            isHorz ?
            gridSize.y / gridLength.y :
            gridSize.y / gridLength.x;

        if (isSquared)
        {
            float cellWidth = horz < vert ? horz : vert;
            cellSize = new Vector2(cellWidth, cellWidth);
            return;
        }

        cellSize = new Vector2(horz, vert);
    }
}
