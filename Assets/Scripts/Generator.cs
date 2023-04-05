using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject boardS;
    public GameObject boardT;
    public Common common;
    public GameObject vertex;
    public GameObject line;

    Vector2 sizeS,sizeT;
    Vector3 vertexOffset;
    float rectToCam;
    Vector3 vertexOffsetC;

    void Awake(){
        //do some initializations
        common.loadedImages = 0;
    }
    public void checkIfGen()
    {
        if(common.loadedImages == 2){
            Generate();
        }
    }
    void Generate()
    {   
        sizeS = boardS.GetComponent<RectTransform>().sizeDelta;
        sizeT = boardT.GetComponent<RectTransform>().sizeDelta;

        vertexOffset = new Vector3(-common.vertexSize/2, common.vertexSize/2, 0);
        rectToCam = mainCamera.GetComponent<Camera>().orthographicSize / gameObject.GetComponent<RectTransform>().sizeDelta.y;
        vertexOffsetC = vertexOffset * rectToCam  *2;

        float canvasWidth = gameObject.GetComponent<RectTransform>().sizeDelta.x;
        float canvasHeight = gameObject.GetComponent<RectTransform>().sizeDelta.y;
        float deltaX = sizeS.x / (common.size.x - 1);
        float deltaY = sizeS.y / (common.size.y - 1);
        #region Generate Grids
        //generate source grid
        common.gridS = new GameObject[common.size.x,common.size.y];
        for(int j = 0; j < common.size.y; j++){
            for(int i = 0; i < common.size.x; i++){
                common.gridS[i,j] = Instantiate(vertex);
                common.gridS[i,j].GetComponent<Vertex>().gridCoord = new Vector2Int(i,j);
                common.gridS[i,j].GetComponent<Vertex>().isSource = true;
                common.gridS[i,j].transform.SetParent(boardS.transform);
                common.gridS[i,j].transform.localScale = new Vector3(1,1,1);
                common.gridS[i,j].GetComponent<RectTransform>().anchoredPosition = 
                    new Vector2(i * deltaX - sizeS.x/2 + common.vertexSize/2, 
                    j * deltaY - sizeS.y/2 - common.vertexSize/2);                
            }
        }
        
        //generate target grid 
        deltaX = sizeT.x / (common.size.x - 1);
        deltaY = sizeT.y / (common.size.y - 1);

        common.gridT = new GameObject[common.size.x,common.size.y];
        for(int j = 0; j < common.size.y; j++){
            for(int i = 0; i < common.size.x; i++){
                common.gridT[i,j] = Instantiate(vertex);
                common.gridT[i,j].GetComponent<Vertex>().gridCoord = new Vector2Int(i,j);
                common.gridT[i,j].GetComponent<Vertex>().isSource = false;
                common.gridT[i,j].transform.SetParent(boardT.transform);
                common.gridT[i,j].transform.localScale = new Vector3(1,1,1);
                common.gridT[i,j].GetComponent<RectTransform>().anchoredPosition = 
                    new Vector2(i * deltaX - sizeT.x/2 + common.vertexSize/2 , 
                    j * deltaY - sizeT.y/2 - common.vertexSize/2 );
                
            }
        }
        #endregion
        #region Draw Lines
        //draw lines for source grid
        common.lineS = new GameObject[common.size.x + common.size.y];
        for(int i=0; i < common.size.y + common.size.x; i++){
            common.lineS[i] = Instantiate(line);
            common.lineS[i].transform.SetParent(boardS.transform);
            UpdateLineS(i);
        }
        //draw lines for target coord
        common.lineT = new GameObject[common.size.x + common.size.y];
        for(int i=0; i < common.size.y + common.size.x; i++){
            common.lineT[i] = Instantiate(line);
            common.lineT[i].transform.SetParent(boardT.transform);
            UpdateLineT(i);
        }
        #endregion
        #region set the sprite
        //boardS.GetComponent<SpriteRenderer>().size = sizeS;
        //boardT.GetComponent<SpriteRenderer>().size = sizeT;
        #endregion
    }

    public void UpdateLineS(int i){
        common.lineS[i].transform.localScale = new Vector3(1,1,1);
        LineRenderer renderer = common.lineS[i].GetComponent<LineRenderer>();
        renderer.startWidth = common.lineWidth; renderer.endWidth = common.lineWidth;
        if(i < common.size.y){//if horizonal
            int n = i;
            renderer.positionCount = common.size.x;
            Vector3[] vertices = new Vector3[renderer.positionCount];
            for(int j  = 0; j < renderer.positionCount; j++){
                vertices[j] = common.gridS[j,n].GetComponent<RectTransform>().position + vertexOffsetC;
                vertices[j].z = 0;
            }
            renderer.SetPositions(vertices);
        }else{//if vertical
            int n = i - common.size.y;
            renderer.positionCount = common.size.y;
            Vector3[] vertices = new Vector3[renderer.positionCount];
            for(int j  = 0; j < renderer.positionCount; j++){
                vertices[j] = common.gridS[n,j].GetComponent<RectTransform>().position + vertexOffsetC;
                vertices[j].z = 0;
            }               
            renderer.SetPositions(vertices);
        }
    }

    public void UpdateLineT(int i){
        common.lineT[i].transform.localScale = new Vector3(1,1,1);
        LineRenderer renderer = common.lineT[i].GetComponent<LineRenderer>();
        renderer.startWidth = common.lineWidth; renderer.endWidth = common.lineWidth;
        if(i < common.size.y){//if horizonal
            int n = i;
            renderer.positionCount = common.size.x;
            Vector3[] vertices = new Vector3[renderer.positionCount];
            for(int j  = 0; j < renderer.positionCount; j++){
                vertices[j] = common.gridT[j,n].GetComponent<RectTransform>().position + vertexOffsetC;
                vertices[j].z = 0;
            }
            renderer.SetPositions(vertices);
        }else{//if vertical
            int n = i - common.size.y;
            renderer.positionCount = common.size.y;
            Vector3[] vertices = new Vector3[renderer.positionCount];
            for(int j  = 0; j < renderer.positionCount; j++){
                vertices[j] = common.gridT[n,j].GetComponent<RectTransform>().position + vertexOffsetC;
                vertices[j].z = 0;
            }               
            renderer.SetPositions(vertices);
        }
    }
}
