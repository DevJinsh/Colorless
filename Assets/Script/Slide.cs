using UnityEngine;
using UnityEngine.EventSystems;

public class Slide : MonoBehaviour
{
    public GameObject[] SlideView;
    int[] Max_X = new int[4] {560, 375, 93, 234};
    Vector3 ClickPos;
    bool isDrag = false;
    
    private void Update()
    {
        for (int i = 0; i < SlideView.Length; i++)
        {
            if(Scene.Active_Scene.Equals(SlideView[i].name))
            {
                Scene.isSlideBackground = true;
                if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
                {
                    isDrag = true;
                    ClickPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                }
                else if (isDrag && Input.GetMouseButtonUp(0))
                {
                    isDrag = false;
                }
                else if (isDrag)
                {                    
                    Vector3 mosPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                    Vector3 Backpos = SlideView[i].transform.position;
                    float Direction = mosPos.x - ClickPos.x;
                    ClickPos = mosPos;
                    if (Direction > 0 && Backpos.x < Max_X[i])
                    {
                        Backpos = new Vector3(Backpos.x + 3, Backpos.y, Backpos.z);
                    }
                    else if (Direction < 0 && Backpos.x > -Max_X[i])
                    {
                        Backpos = new Vector3(Backpos.x - 3, Backpos.y, Backpos.z);
                    }
                    else
                    {
                        Backpos = new Vector3(Backpos.x, Backpos.y, Backpos.z);
                    }
                    SlideView[i].transform.position = Backpos;
                }
            }
        }
    }
}
