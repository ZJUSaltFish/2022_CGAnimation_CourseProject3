using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
//注：这个脚本已经弃用。
/*
public class ImageInitialize : MonoBehaviour
{
    public Common common;
    //public GameObject image;
    public GameObject boardS;
    public GameObject boardT;

    List<Vector2> imageSizeList = new List<Vector2>();
    List<Sprite> spriteList = new List<Sprite>();
    String[] files;

    void Awake()
    {
        //boardS = gameObject.GetComponent<Generator>().boardS;
        //boardT = gameObject.GetComponent<Generator>().boardT;
        GetSprites();
        //Debug.Log("GG");
    }
    void GetSprites(){//Debug.Log(Directory.Exists("./Assets"));
        files = Directory.GetFiles(@".\Assets\Images", "*.jpg");
        if(files[0] != null && files[1] != null){
            LoadSprite(files[0]);
            LoadSprite(files[1]);

            boardS.GetComponent<RectTransform>().sizeDelta = imageSizeList[0];
            boardT.GetComponent<RectTransform>().sizeDelta = imageSizeList[1];

            boardS.GetComponent<Image>().sprite = spriteList[0];
            boardT.GetComponent<Image>().sprite = spriteList[1];
        }
    }

    void LoadSprite(string path){
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        fs.Seek(0, SeekOrigin.Begin);
        byte[] bytes = new byte[fs.Length];
        try{
            fs.Read(bytes, 0, bytes.Length);
        }catch(Exception exception){
            Debug.Log(exception);
        }
        fs.Close();
        //Debug.Log(bytes.Length);
        Texture2D texture = new Texture2D(common.imageWidth, common.imageHeight); 
        if(texture.LoadImage(bytes)){
            //Debug.Log(texture.GetPixel(101,101));
            imageSizeList.Add(new Vector2(common.imageWidth,common.imageHeight));
            Sprite sprite = Sprite.Create(texture, 
                new Rect(0,0,boardS.GetComponent<RectTransform>().sizeDelta.x, boardS.GetComponent<RectTransform>().sizeDelta.y), new Vector2(0.5f, 0.5f));
            spriteList.Add(sprite);
        }else{
            Debug.Log("Failed to Load Image");
        }
    }
}
*/
