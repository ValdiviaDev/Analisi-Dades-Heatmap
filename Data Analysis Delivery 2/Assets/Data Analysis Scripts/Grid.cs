﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System.Globalization;

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
    public GameObject HeatMapCube;
    public Material matCube;
    public Gradient gradient;
    public GradientColorKey[] colorKey;
    public GradientAlphaKey[] alphaKey;

    float posx = 0.0f;
    float posy = 0.0f;
    float posz = 0.0f;
    void Start()
    {
        cubes_heatmap = new Dictionary<Vector2, GameObject>();
        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];

        SetGrid();
        SetCSVValues();
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
                debugTextArray[x, y] = CreateWorldText(gridArray[x, y].ToString(), GetWorldPos(x, y) + new Vector3(cubeSize, 0, cubeSize) * 0.5f, 1, Color.blue, TextAnchor.MiddleCenter);
                //Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x, y + 1), Color.white, 100f);
                //Debug.DrawLine(GetWorldPos(x, y), GetWorldPos(x + 1, y), Color.white, 100f);
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
            Vector3[] points = new Vector3[5];
        }
        else
        {
            go = Instantiate(HeatMapCube, new Vector3(x * cubeSize + cubeSize / 2, 2, y * cubeSize + cubeSize / 2), Quaternion.identity);
            go.transform.SetParent(grid_parent.transform);
            go.transform.localScale *= cubeSize;
            cubes_heatmap.Add(gridPos, go);
            go.GetComponent<MeshRenderer>().material = matCube;
        }

        if (go)
        {
            float val = GetValue(x, y) * 0.01f;

            Color c = gradient.Evaluate(val);

            Debug.Log("Val: " + val.ToString() + "Color" + c.ToString());

            go.GetComponent<MeshRenderer>().material.color = c;
        }
    }

    public void SetCSVValues()
    {

        if (System.IO.File.Exists("Assets/CSV/positions.csv"))
        {
            List<string> stringList = new List<string>();
            List<string[]> parsedList = new List<string[]>();
            // List<Vector3> pos_list = new List<Vector3>();

            StreamReader str_reader = new StreamReader("Assets/CSV/positions.csv");
            while (!str_reader.EndOfStream)
            {
                string line = str_reader.ReadLine();
                stringList.Add(line);
            }
            str_reader.Close();

            for (int i = 1; i < stringList.Count; i++)
            {
                string[] temp = stringList[i].Split(';');

                for (int j = 0; j < temp.Length; j++)
                {
                    temp[j] = temp[j].Trim();

                    if (j == 2)
                    {
                        posx = float.Parse(temp[j]);

                    }

                    if (j == 3)
                    {
                        posy = float.Parse(temp[j]);
                    }

                    if (j == 4)
                    {
                        posz = float.Parse(temp[j]);
                    }

                    Vector3 pos = new Vector3
                    (
                        posx,
                        posy,
                        posz
                    );

                    SetValue(pos, GetValue(pos) + 5);

                    int x, y;
                    GetXY(pos, out x, out y);
                    UpdateHeatmap(x, y);
                }
                parsedList.Add(temp);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
