using System.Collections;
using UnityEngine;

public class Scene : MonoBehaviour
{
    private static Scene scene;

    public static GameObject[] Entire_Background;
    public static GameObject[] Props;
    static GameObject[] background;
    public static int Current_Background = 0;

    public static GameObject[] scenes;
    string Start_Scene = "로비";
    public static string Active_Scene = "";
    public static string Previous_Scene = "";

    public static bool isSlideBackground = false;

    private void Awake() => scene = this;
    void Start()
    {
        scenes = GameObject.FindGameObjectsWithTag("Scene");
        Entire_Background = GameObject.FindGameObjectsWithTag("Background");
        Props = GameObject.FindGameObjectsWithTag("Prop");
        Change_Scene(Start_Scene);
    }

    void Update()
    {
        for(int i = 0; i < background.Length; i++)
        {
            if (Current_Background.Equals(i))
            {
                background[i].SetActive(true);
            }
            else
            {
                background[i].SetActive(false);
            }
        }
    }
    public static void Change_Scene(string nextscene)
    {
        if (nextscene.Equals("로비_계단"))
        {
            if (!UI.open_Cardread)
            {
                Subtitles.Load_Subtitles("문이 잠겨있다.");
                return;
            }
        }
        else if(nextscene.Equals("복도_유전자공학실"))
        {
            Subtitles.Load_Subtitles("문이 잠겨있다.");
            return;
        }
        else if(nextscene.Equals("오물처리장"))
        {
            if (!Puzzle_Pattern.Clear)
            {
                Subtitles.Load_Subtitles("문이 잠겨있다.");
                return;
            }            
        }
        if(background != null)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].SetActive(true);
            }
        }
        Previous_Scene = Active_Scene;
        scene.SceneChanging(nextscene);
    }

    void SceneChanging(string next)
    {        
        Active_Scene = next;
        isSlideBackground = false;
        for (int i = 0; i < scenes.Length; i++)
        {            
            if (Active_Scene.Equals(scenes[i].name))
            {
                Title.Load_Title();
                scenes[i].SetActive(true);
                Current_Background = 0;
            }
            else
            {
                scenes[i].SetActive(false);
            }            
        }
        background = GameObject.FindGameObjectsWithTag("Background");
    }
}
