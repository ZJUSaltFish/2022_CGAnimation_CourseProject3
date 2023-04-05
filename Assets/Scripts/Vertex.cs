using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Vertex : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    // Start is called before the first frame update
    public Common common;
    public Vector2Int gridCoord;
    public bool isSource;
    private Vector2 offset;
    void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(common.vertexSize, common.vertexSize);
    }
    public void OnPointerDown(PointerEventData eventData){
        offset = gameObject.GetComponent<RectTransform>().position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //gameObject.GetComponent<RectTransform>().position = new Vector2(Camera.main.ScreenToWorldPoint(mousePos).x, Camera.main.ScreenToWorldPoint(mousePos).y);
    }
    public void OnDrag(PointerEventData eventData){
        Vector2Int gridCoord = gameObject.GetComponent<Vertex>().gridCoord;
        if(gridCoord.x != 0 && gridCoord.x != common.size.x-1 && gridCoord.y != 0 && gridCoord.y != common.size.y -1){//boundary is not movable
            gameObject.GetComponent<RectTransform>().position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y) + offset;
            Vector3 vertexOffset = new Vector3(-common.vertexSize/2, common.vertexSize/2, 0);
            if(isSource){
                gameObject.transform.parent.transform.parent.gameObject.GetComponent<Generator>().UpdateLineS(gridCoord.y);
                gameObject.transform.parent.transform.parent.gameObject.GetComponent<Generator>().UpdateLineS(gridCoord.x + common.size.y);
                //common.lineS[gridCoord.y].GetComponent<LineRenderer>().SetPosition(gridCoord.x,gameObject.GetComponent<RectTransform>().position + vertexOffset);
                //common.lineS[common.size.y + gridCoord.x].GetComponent<LineRenderer>().SetPosition(gridCoord.y,gameObject.GetComponent<RectTransform>().position + vertexOffset);
            }else{
                gameObject.transform.parent.transform.parent.gameObject.GetComponent<Generator>().UpdateLineT(gridCoord.y);
                gameObject.transform.parent.transform.parent.gameObject.GetComponent<Generator>().UpdateLineT(gridCoord.x + common.size.y);
                //common.lineT[gridCoord.y].GetComponent<LineRenderer>().SetPosition(gridCoord.x,gameObject.GetComponent<RectTransform>().position + vertexOffset);
                //common.lineT[common.size.y + gridCoord.x].GetComponent<LineRenderer>().SetPosition(gridCoord.y,gameObject.GetComponent<RectTransform>().position + vertexOffset);
            }
        }           
    }
}
