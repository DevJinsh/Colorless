using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUI : MonoBehaviour
{
    public void Right_Button()
    {
        if (Scene.Current_Background < 3)
            Scene.Current_Background += 1;
        else
            Scene.Current_Background = 0;
    }

    public void Left_Button()
    {
        if (Scene.Current_Background > 0)
            Scene.Current_Background -= 1;
        else
            Scene.Current_Background = 3;
    }
    public void Back_Button()
    {
        if (Scene.Active_Scene.Equals("복도"))
        {
            Scene.Change_Scene("복도_계단");
        }
        else
        {
            Scene.Change_Scene(Scene.Previous_Scene);
        }
    }
    public void BackLeft_Button()
    {
        Scene.Change_Scene("동물임상실험실");
    }
    public void BackRight_Button()
    {        
        Scene.Change_Scene("실험실2");
    }
}
