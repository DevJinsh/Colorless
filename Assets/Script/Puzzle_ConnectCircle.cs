using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle_ConnectCircle : MonoBehaviour
{
    public Canvas canvas;
    public GameObject LinePrefab;

    bool isunLocking = false;
    public static bool Clear = false;

    private List<GameObject> Circles;
    private List<GameObject> Lines;

    private GameObject lineOnEdit;
    private RectTransform lineOnEditRcTs;
    private GameObject circleOnEdit;
    Color LineColor;
    private void Start()
    {
        Circles = new List<GameObject>();
        Lines = new List<GameObject>();
    }
    void Update()
    {
        if (isunLocking)
        {
            Vector3 mousePos = canvas.transform.InverseTransformPoint(Input.mousePosition);

            lineOnEditRcTs.sizeDelta = new Vector2(lineOnEditRcTs.sizeDelta.x, Vector3.Distance(mousePos, circleOnEdit.transform.localPosition));
            lineOnEditRcTs.rotation = Quaternion.FromToRotation(Vector3.up, (mousePos - circleOnEdit.transform.localPosition).normalized);
        }
    }

    void TrySetLineEdit(GameObject obj)
    {
        foreach (var circle in Circles)
        {
            if (circle.name.Equals(obj.name))
            {
                return;
            }
        }

        lineOnEdit = CreateLine(obj.GetComponent<RectTransform>().localPosition, obj);
        lineOnEdit.GetComponent<Image>().color = LineColor;
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
        if (!Clear)
        {
            isunLocking = true;

            LineColor = obj.GetComponent<Image>().color;

            TrySetLineEdit(obj);            
        }
    }
    public void OnMouseUpCircle(GameObject obj)
    {
        isunLocking = false;

        if(LineColor != circleOnEdit.GetComponent<Image>().color)
        {
            foreach (var line in Lines)
            {
                Destroy(line);
            }

            Circles.Clear();
        }

        Lines.Clear();

        lineOnEdit = null;
        LineColor = Color.white;
        lineOnEditRcTs = null;
        circleOnEdit = null;
    }
}
