using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class ButtonClick : MonoBehaviour
{
    public void Click(){
        //Debug.Log("Clicked");
        DisableBtn();
        DestroyImmediate(gameObject.transform.GetChild(0).gameObject);
        gameObject.GetComponent<LoadImage>().Load();
        gameObject.transform.parent.gameObject.GetComponent<Generator>().checkIfGen();
    }
    void DisableBtn(){
       gameObject.GetComponent<Button>().enabled = false;
    }
}
