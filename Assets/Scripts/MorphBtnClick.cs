using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MorphBtnClick : MonoBehaviour
{
    public Common common;
    // Start is called before the first frame update
    public void Click()
    {
        //Debug.Log(common.loadedImages);
        if(common.loadedImages == 2)
        {
            gameObject.transform.parent.gameObject.GetComponent<Morphing>().MorphBegin();

            DestroyImmediate(gameObject);
        }
        
    }
}
