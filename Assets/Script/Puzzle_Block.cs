using UnityEngine;

public class Puzzle_Block : MonoBehaviour
{
    private int[,] Success_Pan = new int[5, 5] {
        {2, 0, 4, 0, 0},
        {1, 0, 0, 0, 0},
        {6, 0, 0, 0, 0},
        {0, 3, 0, 0, 0},
        {7, 0, 0, 5, 0},
    };
    private static int[,] Pan = new int[5, 5];

    int index;
    private GameObject target = null;
    bool isReach = false;

    Vector3 MousePosition;
    Vector3 Static_Position;
    Vector3 Current_Position;

    void Start()
    {
        this.Static_Position = this.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            CastRay();
            if (target != null)
            {
                if (target.name.Equals("새로고침 버튼"))
                {
                    Refresh();
                }
            }
            if (Check_Puzzle() == 7)
            {
                //Player.isClear = true;
            }
            if(isReach)
            {
                this.transform.position = this.Current_Position;
                int Color = Check_Color(this.name);
                Pan[index / 5, index % 5] = Color;
            }
            else
            {
                this.transform.position = this.Static_Position;
                Pan[index / 5, index % 5] = 0;
            }
        }
    }

    void CastRay()
    {
        target = null;
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        if (hit.collider != null)
        {
            target = hit.collider.gameObject;
        }
    }

    void OnMouseDrag()
    {
        MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        transform.position = Camera.main.ScreenToWorldPoint(MousePosition);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Area"))
        {
            Current_Position = other.transform.position;
            index = int.Parse(other.name) - 1;
            isReach = Pan[index / 5, index % 5].Equals(0);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Area"))
        {
            isReach = false;
        }
    }

    int Check_Puzzle()
    {
        int Success_Cnt = 0;
        for (int i = 0; i < 5; i++)
        {
            for (int v = 0; v < 5; v++)
            {
                if (Pan[i, v].Equals(Success_Pan[i, v]) && !Pan[i, v].Equals(0))
                {
                    Success_Cnt++;
                }
            }
        }
        return Success_Cnt;
    }

    int Check_Color(string name)
    {
        int Color = 0;
        if (name.Equals("Red"))
        {
            Color = 1;
        }
        else if (name.Equals("Orange"))
        {
            Color = 2;
        }
        else if (name.Equals("Yellow") )
        {
            Color = 3;
        }
        else if (name.Equals("Green"))
        {
            Color = 4;
        }
        else if (name.Equals("SkyBlue"))
        {
            Color = 5;
        }
        else if (name.Equals("Purple"))
        {
            Color = 6;
        }
        else if (name.Equals("Pink"))
        {
            Color = 7;
        }
        return Color;
    }

    void Refresh()
    {
        isReach = false;
        for (int i = 0; i < 5; i++)
        {
            for (int v = 0; v < 5; v++)
            {
                Pan[i,v] = 0;
            }
        }
    }
}