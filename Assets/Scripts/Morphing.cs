using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Morphing : MonoBehaviour
{
    public Common common;
    public GameObject boardS;
    public GameObject boardT;
    Texture2D imageS;
    Texture2D imageT;

    Texture2D[] morphingImg;//images to be generated

    Vector2[,] gridS;//the normalized vec2 array corresponding to common.gridS; raw - x(u) & column - y(v)
    Vector2[,] gridT;//the normalized vec2 array corresponding to common.gridT; raw - x(u) & column - y(v)
    Vector2[,] gridM;//the mid-morph grid
    Vector2[,] pixelCoordM;//the uv coord of each pixel on the grid surface 
    public void MorphBegin()
    {//Debug.Log("HIA");
        imageS = (Texture2D)boardS.GetComponent<RawImage>().texture;
        imageT = (Texture2D)boardT.GetComponent<RawImage>().texture;
        
        if(imageS != null && imageT != null)
        {
            Morph();
        }
    }

    void Morph(){
        morphingImg = new Texture2D[common.morphIterations];
        
        GetNormailzedVertices();
        for(int i = 0; i <= common.morphIterations+1; i++)//for each iteration
        {
            GetMidVertices(i);//lerp gridS & gridT to get gridM
            //Debug.Log(i);
            GetMidPixels(i);//get pixelCoordM
            //Debug.Log("HI");
        }
        
    }

    void GetNormailzedVertices()
    {//Debug.Log("HI");
        gridS = new Vector2[common.size.x, common.size.y];
        gridT = new Vector2[common.size.x, common.size.y];
        for(int v=0; v< common.size.y; v++)
        {
            for(int u=0; u< common.size.x; u++)
            {
                //source
                gridS[u,v] = common.gridS[u,v].GetComponent<RectTransform>().anchoredPosition
                    - common.gridS[0,0].GetComponent<RectTransform>().anchoredPosition; //convert to local coord
                    //normalize
                gridS[u,v].x /= common.gridS[common.size.x -1,common.size.y -1].GetComponent<RectTransform>().anchoredPosition.x
                    - common.gridS[0,0].GetComponent<RectTransform>().anchoredPosition.x;
                gridS[u,v].y /= common.gridS[common.size.x -1,common.size.y -1].GetComponent<RectTransform>().anchoredPosition.y
                    - common.gridS[0,0].GetComponent<RectTransform>().anchoredPosition.y;
                //target
                gridT[u,v] = common.gridT[u,v].GetComponent<RectTransform>().anchoredPosition
                    - common.gridT[0,0].GetComponent<RectTransform>().anchoredPosition; //convert to local coord
                    //normalize
                gridT[u,v].x /= common.gridT[common.size.x -1,common.size.y -1].GetComponent<RectTransform>().anchoredPosition.x
                    - common.gridT[0,0].GetComponent<RectTransform>().anchoredPosition.x;
                gridT[u,v].y /= common.gridT[common.size.x -1,common.size.y -1].GetComponent<RectTransform>().anchoredPosition.y
                    - common.gridT[0,0].GetComponent<RectTransform>().anchoredPosition.y;
                //Debug.Log(gridT[u,v]);
            }
        }
    }

    void GetMidVertices(int iterations)
    {
        gridM = new Vector2[common.size.x,common.size.y];
        float lerp = (float)iterations / (float)(common.morphIterations + 2);// [0, {morphIterations}, 1]
        for(int v = 0; v < common.size.y; v++)
        {
            for(int u = 0; u < common.size.x; u++)
            {
                gridM[u,v] = gridS[u,v] * (1-lerp) + gridT[u,v] * lerp; 
                
            }
        }
        //Debug.Log(gridM[3,5]);
    }

    void GetMidPixels(int iterations)
    {   
        float lerp = iterations / (common.morphIterations + 2);
        int widthS = imageS.width, heightS = imageS.height;
        int widthT = imageT.width, heightT = imageT.height;

        int widthM = (int)(widthS * (1-lerp) + widthT * lerp);
        int heightM = (int)(heightS * (1-lerp) + heightT * lerp);
        pixelCoordM = new Vector2[widthM,heightM];
        //for each pixel
        for(int y = 0; y < heightM; y++)
        {
            for(int x = 0; x < widthM; x++)
            {
                //gradient descent
                Vector2 xyCoordM = new Vector2((float)x / widthM, (float)y / heightM);
                Vector2 uvCoordM = new Vector2(0.5f, 0.5f);
                float err = 1;//int k=1;
                while(Mathf.Abs(err) >= common.tolerance)
                {
                    uvCoordM += GetDescentAt(uvCoordM);
                    err = (xyCoordM - GetXYCoordAt(uvCoordM)).magnitude;                  
                }
                //Debug.Log(err);
            }
        }
        //Debug.Log(widthM);
    }

    Vector2 GetDescentAt(Vector2 uvCoord)
    {
        Vector2 descent = new Vector2(0,0);
        for(int j=0; j<common.size.y; j++)
        {
            for(int i=0; i<common.size.x; i++)
            {
                //xyCoord += gridM[i,j] * Bernstein(i, uvCoord.x, common.size.x-1) * Bernstein(j, uvCoord.y, common.size.y-1);
            }
        }
        return descent;
    }

    Vector2 GetXYCoordAt(Vector2 uvCoord)
    {
        Vector2 xyCoord = new Vector2(0,0);
        for(int j=0; j<common.size.y; j++)
        {
            for(int i=0; i<common.size.x; i++)
            {
                xyCoord += gridM[i,j] * Bernstein(i, uvCoord.x, common.size.x-1) * Bernstein(j, uvCoord.y, common.size.y-1);
            }
        }
        return xyCoord;
    }

    float Bernstein(int i, float u, int n)
    {
        float B;
        B = Mathf.Pow(u, i) * Mathf.Pow(1-u, n-i) * Combinatorial(i, n);
        return B;
    }

    int Combinatorial(int r, int n)
    {
        int C=1;
        for(int i=n; i>n-r; i--)
        {
            C *= i;
        }
        for(int i=r; i>1; i--)
        {
            C /= i;
        }
        return C;
    }
}
