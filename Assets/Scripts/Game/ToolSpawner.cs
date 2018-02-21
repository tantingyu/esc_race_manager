using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ToolSpawner : MonoBehaviour {

    public Tilemap tilemap;
    public GameObject toolPrefab;
    public Vector3Int cell2;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);  //Store the position in a vector
            Vector3Int cell = tilemap.WorldToCell(worldPos); //cell num is int
            
            cell2.x = (int)((cell.x+7)/3)*3-7;
            cell2.y = (int)((cell.y)/3)*3;
            cell2.z = 0;
            Debug.Log(cell);
            Debug.Log(cell2);
            Vector3 cellCenterpos = tilemap.GetCellCenterWorld(cell2);

            Instantiate(toolPrefab, cellCenterpos, Quaternion.identity);
        }
    }
}
