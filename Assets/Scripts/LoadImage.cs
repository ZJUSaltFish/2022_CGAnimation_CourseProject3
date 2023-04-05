using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Runtime.InteropServices;
public class LoadImage : MonoBehaviour
{
    public Common common;
    //Vector2 ImageSIze;
    //Texture2D Image;
    
    public void Load(){
        Debug.Log("ImageLoading");
        bool isLoading = false;
        if(!isLoading){
            isLoading = true;
            OpenFileName ofn = new OpenFileName();
            InitializeOFN(ofn);
            if(LoacalDialog.GetOFN(ofn)){
                GetImage(ofn.file);
            }
            //Debug.Log("filePath:" + ofn.file);   
        }
        //ofn.dlgOwner = GetActiveWindow();
    }

    void InitializeOFN(OpenFileName ofn){
        ofn.structSize = Marshal.SizeOf(ofn);
        ofn.filter = "图片文件(*.jpg;*.png)\0*.jpg;*.png\0";
        ofn.file = new string(new char[256]);
        ofn.maxFile = ofn.file.Length;
        ofn.fileTitle = new String(new char[64]);
        ofn.maxFileTitle = ofn.fileTitle.Length;
        ofn.initialDir = Application.streamingAssetsPath.Replace('/','\\');
        ofn.title = "Select Image";
        ofn.flags = 0x0008000 | 0x0001000 | 0x0000800 | 0x00000008;
    }

    void GetImage(String path)
    {
        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
        fs.Seek(0, SeekOrigin.Begin);
        byte[] bytes = new byte[fs.Length];
        try{
            fs.Read(bytes, 0, bytes.Length);
        }catch(Exception exception){
            Debug.Log(exception);
        }
        fs.Close();

        int thisWidth = (int)gameObject.GetComponent<RectTransform>().sizeDelta.x;
        int thisHeight = (int)gameObject.GetComponent<RectTransform>().sizeDelta.y;
        
        Texture2D texture = new Texture2D(thisWidth, thisHeight);
        if(texture.LoadImage(bytes)){
            //Debug.Log(texture.GetPixel(101,101));
            gameObject.GetComponent<RawImage>().texture = texture;
            Vector2 size = new Vector2(Mathf.Clamp(thisWidth*texture.width/texture.height, 0, thisWidth), thisHeight);
            gameObject.GetComponent<RectTransform>().sizeDelta = size;
            
            common.loadedImages += 1;
        }else{
            Debug.Log("Failed to Load Image");
        }
    }

}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class OpenFileName
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public string customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public string initialDir = null;
    public string title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr receivedPtr = IntPtr.Zero;
    public int receivedInt = 0;
    public int flagsEx = 0;

}

public class LoacalDialog
{
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] OpenFileName openFileName);
    public static bool GetOFN([In, Out] OpenFileName openFileName){
        return GetOpenFileName(openFileName);
    }
}