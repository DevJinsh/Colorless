using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_Pattern : MonoBehaviour
{
    bool isunLocking = false;
    public static bool Clear = false;

    public Canvas canvas;
    public GameObject LinePrefab;

    private List<GameObject> Lines;
    private List<GameObject> Circles;
    private List<string> Currect_Puzzle = new List<string>() {"5", "1", "8", "3", "4", "9", "2", "7", "6"};

    private GameObject lineOnEdit;
    private RectTransform lineOnEditRcTs;
    private GameObject circleOnEdit;

    // Start is called before the first frame update
    void Start()
    {
        Lines = new List<GameObject>();
        Circles = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isunLocking)
        {
            Vector3 mousePos = canvas.transform.InverseTransformPoint(Input.mousePosition);

            lineOnEditRcTs.sizeDelta = new Vector2(lineOnEditRcTs.sizeDelta.x, Vector3.Distance(mousePos, circleOnEdit.transform.localPosition));
            lineOnEditRcTs.rotation = Quaternion.FromToRotation(Vector3.up, (mousePos - circleOnEdit.transform.localPosition).normalized);
        }
    }

    void TrySetLineEdit(GameObject obj)
    {
        foreach(var circle in Circles)
        {
            if(circle.name.Equals(obj.name))
            {
                return;
            }
        }

        lineOnEdit = CreateLine(obj.transform.localPosition, obj);
        lineOnEditRcTs = lineOnEdit.GetComponent<RectTransform>();
        circleOnEdit = obj;
    }
    GameObject CreateLine(Vector3 pos, GameObject obj)
    {
        var line = GameObject.Instantiate(LinePrefab, canvas.transform);

        line.transform.localPosition = pos;

        Lines.Add(line);
        Circles.Add(obj);

        return line;
    }

    void Check_Puzzle()
    {
        if (!Circles.Count.Equals(Currect_Puzzle.Count))
            return;
        for(int i = 0; i < Currect_Puzzle.Count; i++)
        {
            if(!Currect_Puzzle[i].Equals(Circles[i].name))
            {
                return;
            }
        }
        InvokeRepeating("Welcome", 0.5f, 0.5f);
        Invoke("InvokeCancel", 2f);
    }
    public void OnMouseEnterCircle(GameObject obj)
    {
        if (isunLocking)
        {
            lineOnEditRcTs.sizeDelta = new Vector2(lineOnEditRcTs.sizeDelta.x, Vector3.Distance(circleOnEdit.transform.localPosition, obj.transform.localPosition));
            lineOnEditRcTs.rotation = Quaternion.FromToRotation(Vector3.up, (obj.transform.localPosition - circleOnEdit.transform.localPosition).normalized);

            TrySetLineEdit(obj);
        }

    }
    public void OnMouseDownCircle(GameObject obj)
    {
        if(!Clear)
        {
            isunLocking = true;

            TrySetLineEdit(obj);
        }        
    }
    public void OnMouseUpCircle(GameObject obj)
    {
        isunLocking = false;

        foreach(var line in Lines)
        {
            Destroy(line.gameObject);
        }

        Check_Puzzle();

        Lines.Clear();
        Circles.Clear();

        lineOnEdit = null;
        lineOnEditRcTs = null;
        circleOnEdit = null;
    }

    void Welcome()
    {
        GameObject Welcome_Image = GameObject.Find("패턴인식기 메세지");
        Subtitles.Load_Subtitles("문이 열렸습니다.");
        Welcome_Image.GetComponent<Image>().enabled = !Welcome_Image.GetComponent<Image>().enabled;
    }

    void InvokeCancel()
    {
        CancelInvoke("Welcome");
        Clear = true;
    }
}
