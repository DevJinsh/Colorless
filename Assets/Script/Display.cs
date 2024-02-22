using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Display : MonoBehaviour
{
    static GameObject[] Interactions;
    static GameObject Interaction_Background;
    public static string Interaction_target;

    static GameObject Inven;
    static GameObject[] Slot;

    GameObject Back_Buttons;
    GameObject Normal_Buttons;
    GameObject Store_Buttons;

    GameObject Window_Option;
    
    void Awake()
    {
        Interactions = GameObject.FindGameObjectsWithTag("Interaction");
        Interaction_Background = GameObject.Find("상호작용");
        Window_Option = GameObject.Find("옵션");

        Inven = GameObject.Find("인벤토리");
        Slot = GameObject.FindGameObjectsWithTag("Slot");

        Back_Buttons = GameObject.Find("슬라이드 버튼");
        Normal_Buttons = GameObject.Find("일반버튼");
        Store_Buttons = GameObject.Find("창고버튼");
    }

    void Update()
    {
        Display_Button();
        Display_Interaction();
        EscapeDisplay();
    }
    
    void Display_Button()
    {
        if(Scene.Active_Scene.Equals("로비_계단") || Scene.Active_Scene.Equals("복도_계단"))
        {
            Back_Buttons.SetActive(false);
            Normal_Buttons.SetActive(false);
        }
        else
        {
            Normal_Buttons.SetActive(!Scene.isSlideBackground);
            Back_Buttons.SetActive(Scene.isSlideBackground);
        }
        if(Scene.Active_Scene.Equals("창고"))
        {
            Store_Buttons.SetActive(true);
            Back_Buttons.SetActive(false);
        }
        else
        {
            Store_Buttons.SetActive(false);
        }
    }

    public void Display_Item()
    {
        for(int i = 0; i < Player.Inventory.Count; i++)
        {
            Image slotimage = Slot[i].GetComponent<Image>();
            slotimage.color = Color.white;
            if(Player.Inventory[i].GetComponent<SpriteRenderer>() != null)
            {
                slotimage.sprite = Player.Inventory[i].GetComponent<SpriteRenderer>().sprite;
            }
            else if(Player.Inventory[i].GetComponent<Image>() != null)
            {
                slotimage.sprite = Player.Inventory[i].GetComponent<Image>().sprite;
            }
        }
    }

    public static void Destroy_Item()
    {
        Slot[Player.Inventory.Count].GetComponent<Image>().sprite = null;
        Slot[Player.Inventory.Count].GetComponent<Image>().color = Color.clear;
    }

    void Display_Interaction()
    {        
        Window_Option.SetActive(Player.Paused);
        Inven.SetActive(Player.ShowInventory);
        Interaction_Background.SetActive(Player.isInteraction);
        if (Player.ShowInventory)
        {
            Display_Item();
        }
        else if(Player.isInteraction)
        {            
            for (int i = 0; i < Interactions.Length; i++)
            {
                if (Interactions[i].name.Equals(Interaction_target))
                {
                    Interactions[i].SetActive(true);
                }
                else
                {
                    Interactions[i].SetActive(false);
                }
            }
        }        
    }

    void EscapeDisplay()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Player.ShowInventory)
            {
                Player.ShowInventory = false;
            }
            else if(Player.isInteraction)
            {
                Player.isInteraction = false;
            }
        }
    }

    public static void Clear_Interaction(string target)
    {
        if (target == null)
            Player.isInteraction = false;
        else
        {
            if (!target.Equals("약물보관소_열쇠(빨강)"))
            {
                Player.isInteraction = false;
            }
            else
            {
                GameObject.Find(target).GetComponent<Image>().enabled = true;
            }            
            GameObject.Find(target).GetComponent<Animator>().SetBool("Clear", true);
            GameObject.Find(target).GetComponent<AudioSource>().PlayOneShot(GameObject.Find(target).GetComponent<AudioSource>().clip);
        }
    }

    public static void Open_Cabinet()
    {
        GameObject.Find("캐비닛").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("실험실1/캐비닛/실험실1_캐비닛(열림)");
        GameObject.Find("캐비닛").GetComponent<BoxCollider2D>().enabled = true;
        GameObject.Find("자물쇠").GetComponent<BoxCollider2D>().enabled = false;
    }
}
