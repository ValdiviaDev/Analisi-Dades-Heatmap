﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    // Start is called before the first frame update
    public int width;
    public int height;
    public float cubeSize;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    private Dictionary<Vector2, GameObject> cubes_heatmap;
    public GameObject grid_parent;

    float posx = 0.0f;
    float posy = 0.0f;
    float posz = 0.0f;
    void Start()
    {
        cubes_heatmap = new Dictionary<Vector2, GameObject>();
        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        SetGrid();
        //SetCSVValues();
    }

    public Grid(int w, int h, float size)
    {
        this.width = w;
        this.height = h;
        cubeSize = size;

    }

    private void SetGrid()
    {




        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {

                debugTextArray[x, y] = CreateWorldText(gridArray[x, y].ToString(), GetWorldPos(x, y) + new Vector3(cubeSize, 0, cubeSize) * 0.5f, 10, Color.green, TextAnchor.MiddleCenter);
                //Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x, y + 1), Color.red, 100f);
                //Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x + 1, y), Color.red, 100f);

            }

        }

        for (int x = (int)grid_parent.transform.position.x -1 ; x < (int)grid_parent.transform.position.x + 129; x++)
        {
            for (int y = (int)grid_parent.transform.position.z -1; y < (int)grid_parent.transform.position.z + 79; y++)
            {

               
                Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x, y + 1), Color.red, 100f);
                Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x + 1, y), Color.red, 100f);

            }

        }

    }

    private Vector3 GetWorldPos(int x, int y)
    {
        return new Vector3(x, 0, y) * cubeSize;
    }

    private TextMesh CreateWorldText(string text, Vector3 pos, int fontSize, Color color, TextAnchor textAnchor)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        gameObject.transform.SetParent(grid_parent.transform);
        Transform trans = gameObject.transform;
        //trans.SetParent(parent, false);
        trans.localPosition = pos;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.text = text;

        return textMesh;
    }
    public void SetValue(int x, int y, int val)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = val;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }

    }

    public void SetValue(Vector3 worldPos, int value)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        SetValue(x, y, value);


    }

    private void GetXY(Vector3 worldPos, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPos.x / cubeSize);
        y = Mathf.FloorToInt(worldPos.z / cubeSize);

    }


    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];

        }
        else
        {
            return 0;
        }
    }


    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);

        return GetValue(x, y);

    }
    private void UpdateHeatmap(int x, int y)
    {


        GameObject go = null;

        Vector2 gridPos = new Vector2(x, y);

        if (cubes_heatmap.TryGetValue(gridPos, out go))
        {
            //Succes

            Vector3[] points = new Vector3[5];

            //go = (GameObject)Instantiate(null, points, Quaternion.identity);



        }
        else
        {

            //Fail


            go = Instantiate(Resources.Load("_Prefabs/CubeHeatMap") as GameObject, new Vector3(x * cubeSize + cubeSize / 2, 1, y * cubeSize + cubeSize / 2), Quaternion.identity);
            go.transform.SetParent(this.gameObject.transform);
            go.transform.localScale *= cubeSize;
            cubes_heatmap.Add(gridPos, go);
            go.GetComponent<MeshRenderer>().material = Instantiate(Resources.Load("_Materials/HeatMapCube") as Material);



        }

        //if (go)
        //{
        //    float val = GetValue(x, y) * 0.01f;

        //    Color c = go.GetComponent<Grad>().gradient.Evaluate(val);

        //    Debug.Log("Val: " + val.ToString() + "Color" + c.ToString());

        //    go.GetComponent<MeshRenderer>().material.color = c;

        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}