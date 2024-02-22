using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class UI : MonoBehaviour
{

    #region 변수
    public Canvas canvas;
    Vector2 mosPos;
    Vector2 ClickPoint;

    public static bool open_Cardread = false;
    GameObject Cardreader_text;

    public Sprite[] LabList;
    GameObject LabPage;
    public static int Current_Page = 0;

    int Current_Food = 0;
    string[] Cameleon_food = new string[4] { "나비", "파리", "바퀴벌레", "모기" };

    public Sprite[] Dial_Sprite;
    GameObject[] DialObject;
    Image DialImage;
    int[] Dial = new int[4] { 0, 0, 0, 0 };
    int DialIndex;
    Vector3 Wheelpos;

    GameObject Tape;
    public Material material;
    float blurAmount = 10f;
    float zoomAmount = 1f;

    RectTransform Wheel_rect;
    RectTransform rectTransform;
    RectTransform Bars_rect;
    #endregion
    #region Unity LifeSycle
    void Start()
    {
        Cardreader_text = GameObject.Find("리더기글씨");
        DialObject = GameObject.FindGameObjectsWithTag("Dial");
        LabPage = GameObject.Find("페이지");
        Tape = GameObject.Find("테이프");
    }

    void Update()
    {
        //mosPos = canvas.transform.InverseTransformPoint(Input.mousePosition);
        if(Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            mosPos = canvas.transform.InverseTransformPoint(Input.GetTouch(0).position);
        }
        if (rectTransform != null)
        {
            Vector2 distence = Input.GetTouch(0).position - ClickPoint;
            ClickPoint += distence;
            rectTransform.position = new Vector3(rectTransform.position.x + distence.x, rectTransform.position.y + distence.y, 0);
        }
        if (Bars_rect != null)
        {
            if (Bars_rect.localPosition.y >= -600f && Bars_rect.localPosition.y <= 600f || mosPos.y >= -600f && mosPos.y <= 600f)
            {
                Bars_rect.position = new Vector3(Bars_rect.position.x, Input.GetTouch(0).position.y, 0);
            }
            if (Bars_rect.gameObject.name == "초점 조절바")
            {
                blurAmount = (-300 - Bars_rect.localPosition.y) * 0.01f;
            }
            if (Bars_rect.gameObject.name == "줌 조절바")
            {
                zoomAmount = Math.Abs((-800 - Bars_rect.localPosition.y) * 0.005f);
            }
        }
        if (Wheel_rect != null)
        {
            if (Wheelpos.y + 100 < mosPos.y)
            {
                Wheelpos = mosPos;
                if (Dial[DialIndex] >= 9)
                {
                    Dial[DialIndex] = 0;
                }
                else
                {
                    Dial[DialIndex]++;
                }
            }
            else if (Wheelpos.y - 100 > mosPos.y)
            {
                Wheelpos = mosPos;
                if (Dial[DialIndex] <= 0)
                {
                    Dial[DialIndex] = 9;
                }
                else
                {
                    Dial[DialIndex]--;
                }
            }
            DialImage.sprite = Dial_Sprite[Dial[DialIndex]];
        }
        if (LabPage.activeSelf)
        {
            LabPage.GetComponent<Image>().sprite = LabList[Current_Page];
        }
        material.SetFloat("_BlurAmount", blurAmount);
        Tape.transform.localScale = new Vector3(zoomAmount, zoomAmount, Tape.transform.localScale.y);
    }
    #endregion
    #region 인벤토리
    public void Show_Inventory()
    {
        Player.ShowInventory = true;
    }

    public void pressSlot(int index)
    {
        if (Player.Inventory.Count > index && Player.Inventory.Count > 0)
        {
            Player.Handle_Item = Player.Inventory[index];
            Subtitles.Load_Subtitles(Player.Handle_Item.name + "를 장착했습니다.");
        }
        else
        {
            Player.Handle_Item = null;
        }
    }
    public static void Pick_Item(GameObject obj)
    {
        Player.Add_Inventory(obj);
        Subtitles.Load_Subtitles(obj.name + "를 흭득했습니다.");
        obj.SetActive(false);
    }
    #endregion
    #region 카드리더기
    public void Click_CardReader()
    {
        if (!open_Cardread)
        {
            if (Player.Handle_Item == null)
            {
                Subtitles.Load_Subtitles("카드키가 필요하다.");
            }
            else if (!Player.Handle_Item.name.Equals("카드키"))
            {
                Subtitles.Load_Subtitles("카드키가 필요하다.");
            }
            else
            {
                Subtitles.Load_Subtitles("문이 열렸다.");
                Player.Use_Item(Player.Handle_Item);
                InvokeRepeating("Welcome", 0.5f, 0.5f);
                Invoke("InvokeCancel", 2f);
            }
        }
    }

    void Welcome()
    {
        Cardreader_text.GetComponent<Image>().enabled = !Cardreader_text.GetComponent<Image>().enabled;
    }

    void InvokeCancel()
    {
        CancelInvoke("Welcome");
        Display.Clear_Interaction("로비문");
        open_Cardread = true;
    }
    #endregion
    #region 드래그
    public void Start_Drag(GameObject obj)
    {
        rectTransform = obj.GetComponent<RectTransform>();
        ClickPoint = Input.GetTouch(0).position;
    }
    public void End_Drag()
    {
        rectTransform = null;
    }

    public void Drag_BarStart(GameObject obj)
    {
        Bars_rect = obj.GetComponent<RectTransform>();
        
    }

    public void Drag_BarEnd()
    {
        Bars_rect = null;
    }
    #endregion
    #region 달력
    public void pass_calendar(GameObject obj)
    {
        obj.GetComponent<Image>().enabled = true;
    }
    public void invisible_calendar(GameObject obj)
    {
        obj.GetComponent<Image>().enabled = false;
    }
    #endregion
    #region 연구일지
    public void IconClick(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void Paging_LeftButton()
    {
        if (Current_Page > 0)
        {
            Current_Page--;
        }
    }
    public void Paging_RightButton()
    {
        if (Current_Page < 3)
        {
            Current_Page++;
        }
    }
    #endregion
    #region 카멜레온
    public void Feed_Cameleon()
    {
        if (Current_Food < 4)
        {
            if (Player.Handle_Item != null && Player.Handle_Item.name.Equals(Cameleon_food[Current_Food]))
            {
                Subtitles.Load_Subtitles("카멜이가 " + Player.Handle_Item.name + "를 먹었다.");
                Player.Use_Item(Player.Handle_Item);
                Current_Food++;
            }
            else
            {
                Subtitles.Load_Subtitles("카멜이가 먹이를 먹는 것을 거부한다.");
            }
            if (Current_Food.Equals(4))
            {
                Subtitles.Load_Subtitles("카멜이가 열쇠를 뱉었다");
                Display.Clear_Interaction("약물보관소_열쇠(빨강)");
            }
        }
    }
    #endregion
    #region 자물쇠
    public void WheelDial_DragStart(int index)
    {
        DialIndex = index;
        Wheel_rect = DialObject[index].GetComponent<RectTransform>();
        DialImage = DialObject[index].GetComponent<Image>();
        Wheelpos = Wheel_rect.localPosition;
    }

    public void WheelDial_DragStop()
    {
        Wheel_rect = null;
        DialImage = null;
    }

    public void Locker_OpenButton()
    {
        int[] Correct_Num = new int[4] { 0, 7, 1, 8 };
        for (int i = 0; i < 4; i++)
        {
            if (!Dial[i].Equals(Correct_Num[i]))
            {
                Subtitles.Load_Subtitles("비밀번호가 틀린 것 같다.");
                return;
            }
        }
        Subtitles.Load_Subtitles("캐비닛이 열렸다.");
        Display.Open_Cabinet();
        Display.Clear_Interaction(null);
    }
    #endregion
    #region 약물보관소_자물쇠
    public void UnLockColor_Red(GameObject obj)
    {
        if (Player.Handle_Item != null)
        {
            if (Player.Handle_Item.name.Equals("약물보관소_열쇠(빨강)"))
            {
                for (int i = 0; i < Scene.Entire_Background.Length; i++)
                {
                    var material = Scene.Entire_Background[i].GetComponent<SpriteRenderer>().material;
                    
                    if (material != null)
                    {
                        material.SetFloat("_Red", 1f);
                    }
                }
                for (int i = 0; i < Scene.Props.Length; i++)
                {
                    if (Scene.Props[i].GetComponent<SpriteRenderer>() != null)
                    {
                        var props_material = Scene.Props[i].GetComponent<SpriteRenderer>().material;
                        if (props_material != null)
                        {
                            props_material.SetFloat("_Red", 1f);
                        }
                    }
                    else if (Scene.Props[i].GetComponent<Image>() != null)
                    {
                        var props_material = Scene.Props[i].GetComponent<Image>().material;
                        if (props_material != null)
                        {
                            props_material.SetFloat("_Red", 1f);
                        }
                    }
                }
                Subtitles.Load_Subtitles("빨간약을 얻었다.");
                Player.Use_Item(Player.Handle_Item);
                obj.SetActive(false);
                GameObject.Find(obj.name + "-열림").GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                Subtitles.Load_Subtitles("열쇠가 맞지 않는다.");
            }
        }
        else
        {
            Subtitles.Load_Subtitles("열쇠가 필요한 것 같다.");
        }
    }

    public void UnLockColor_Yellow(GameObject obj)
    {
        if (Player.Handle_Item != null)
        {
            if (Player.Handle_Item.name.Equals("약물보관소_열쇠(노랑)"))
            {
                for (int i = 0; i < Scene.Entire_Background.Length; i++)
                {
                    var material = Scene.Entire_Background[i].GetComponent<SpriteRenderer>().material;

                    if (material != null)
                    {
                        material.SetFloat("_Yellow", 1f);
                    }
                }
                for (int i = 0; i < Scene.Props.Length; i++)
                {
                    if (Scene.Props[i].GetComponent<SpriteRenderer>() != null)
                    {
                        var props_material = Scene.Props[i].GetComponent<SpriteRenderer>().material;
                        if (props_material != null)
                        {
                            props_material.SetFloat("_Yellow", 1f);
                        }
                    }
                    else if (Scene.Props[i].GetComponent<Image>() != null)
                    {
                        var props_material = Scene.Props[i].GetComponent<Image>().material;
                        if (props_material != null)
                        {
                            props_material.SetFloat("_Yellow", 1f);
                        }
                    }
                }
                Subtitles.Load_Subtitles("노란약을 얻었다.");
                Player.Use_Item(Player.Handle_Item);
                obj.SetActive(false);
                GameObject.Find(obj.name + "-열림").GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                Subtitles.Load_Subtitles("열쇠가 맞지 않는다.");
            }
        }
        else
        {
            Subtitles.Load_Subtitles("열쇠가 필요한 것 같다.");
        }
    }

    public void UnLockColor_Blue(GameObject obj)
    {
        if (Player.Handle_Item != null)
        {
            if (Player.Handle_Item.name.Equals("약물보관소_열쇠(파랑)"))
            {
                for (int i = 0; i < Scene.Entire_Background.Length; i++)
                {
                    var material = Scene.Entire_Background[i].GetComponent<SpriteRenderer>().material;

                    if (material != null)
                    {
                        material.SetFloat("_Blue", 1f);
                    }
                }
                for (int i = 0; i < Scene.Props.Length; i++)
                {
                    if (Scene.Props[i].GetComponent<SpriteRenderer>() != null)
                    {
                        var props_material = Scene.Props[i].GetComponent<SpriteRenderer>().material;
                        if (props_material != null)
                        {
                            props_material.SetFloat("_Blue", 1f);
                        }
                    }
                    else if (Scene.Props[i].GetComponent<Image>() != null)
                    {
                        var props_material = Scene.Props[i].GetComponent<Image>().material;
                        if (props_material != null)
                        {
                            props_material.SetFloat("_Blue", 1f);
                        }
                    }
                }
                Subtitles.Load_Subtitles("파란약을 얻었다.");
                Player.Use_Item(Player.Handle_Item);
                obj.SetActive(false);
                GameObject.Find(obj.name + "-열림").GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                Subtitles.Load_Subtitles("열쇠가 맞지 않는다.");
            }
        }
        else
        {
            Subtitles.Load_Subtitles("열쇠가 필요한 것 같다.");
        }
    }
    #endregion
    #region 테스트
    public void Test()
    {
        Debug.Log("Click");
    }
    #endregion
}
