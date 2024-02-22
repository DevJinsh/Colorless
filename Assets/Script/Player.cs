using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool ShowInventory = false;
    public static bool isInteraction = false;
    public static bool Paused = false;
    public static GameObject Handle_Item = null;
    public static List<GameObject> Inventory = new List<GameObject>();
    public static bool[] ColorWater = new bool[4] {false, false, false, false};
    public static Sprite[] Slotsprites;
    private void Update()
    {
        if(!isInteraction && !ShowInventory)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (Paused)
                {
                    Time.timeScale = 0f;
                }
                else
                {
                    Time.timeScale = 1f;
                }
                Paused = !Paused;
            }
        }
    }
    public static void Add_Inventory(GameObject item)
    {
        Inventory.Add(item);
    }

    public static void Use_Item(GameObject item)
    {
        Inventory.Remove(item);
        Handle_Item = null;
        Display.Destroy_Item();
    }
}
