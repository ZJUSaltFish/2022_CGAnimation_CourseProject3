using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "common", menuName = "scriptableObject/common", order = 0)]
public class Common : ScriptableObject
{
    #region variables about the grids
    public Vector2Int size;//the degree of the grid
    public float vertexSize;//this defines size of the grid vertex

    public GameObject[,] gridS;//this is the Grid on the Source Image. [x,y]is an integer coordinate
    
    public GameObject[,] gridT;//this is the Grid on the Target Image
    #endregion 

    #region variables about grid lines
    public GameObject[] lineS;//lines conecting source vertices, [horizonal + vertical]
    public GameObject[] lineT;//lines conecting target vertices
    public float lineWidth;//width of a line
    #endregion

    #region variables about morphing
    public int loadedImages = 0;//number of images loaded

    public int morphIterations;//The number of morphed images to generate

        #region variables about Gradient Descent
    public float descent;
    public float tolerance;
        #endregion
    #endregion
}

