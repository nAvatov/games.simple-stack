using System.Collections;
using UnityEngine;

public class Grid3DLayoutComponent : MonoBehaviour
{
    [SerializeField] private int rows = 3; 
    [SerializeField] private int columns = 3; 
    [SerializeField] private int layers = 3; 
    [SerializeField] private float spacing = 1.5f;
    [SerializeField] private GameObject prefab; 

    public void GenerateGrid()
    {
        ClearGrid();
        
        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < layers; y++)
            {
                for (int z = 0; z < columns; z++)
                {
                    Vector3 position = new Vector3(x * spacing, y * spacing, z * spacing);
                    GameObject newObj = Instantiate(prefab, transform);
                    newObj.transform.localPosition = position;
                }
            }
        }
    }

    private void ClearGrid()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
