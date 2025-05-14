using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Grid3DLayoutComponent : MonoBehaviour
{
    [Header("Grid General Size")]
    [SerializeField] private int _rowsAmount = 3; 
    [SerializeField] private int _columnsAmount = 3; 
    [SerializeField] private int _layersAmount = 3; 
    
    [Header("Spacings")]
    [SerializeField] private float _spacingBetweenLayers = 1.5f;
    [SerializeField] private float _spacingBetweenRows = 1.5f;
    [SerializeField] private float _spacingBetweenColumns = 1.5f;
    [SerializeField] [Range(1f, 2f)] private float _extraLayersSpacing = 1f;
    [SerializeField] [Range(1f, 2f)] private float _extraRowsSpacing = 1f;
    [SerializeField] [Range(1f, 2f)] private float _extraColumnsSpacing = 1f;

    [Header("Content")] 
    [SerializeField] private GameObject _gridElementVisualizer;
    [SerializeField] private GameObject _targetSpawnObject; 
    [SerializeField] private BoxCollider _objectsCollider;
    

    public void GenerateGrid()
    {
        _spacingBetweenLayers = _objectsCollider.size.y * _targetSpawnObject.transform.localScale.z * _extraLayersSpacing;
        _spacingBetweenColumns = _objectsCollider.size.z * _targetSpawnObject.transform.localScale.x * _extraColumnsSpacing;
        _spacingBetweenRows = _objectsCollider.size.x * _targetSpawnObject.transform.localScale.y * _extraRowsSpacing;
        
        ClearGrid();
        
        for (int x = 0; x < _rowsAmount; x++)
        {
            for (int y = 0; y < _layersAmount; y++)
            {
                for (int z = 0; z < _columnsAmount; z++)
                {
                    Vector3 position = new Vector3(x * _spacingBetweenRows, y * _spacingBetweenLayers, z * _spacingBetweenColumns);
                    GameObject newObj = Instantiate(_gridElementVisualizer, transform);
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
