using UnityEngine;
using UnityEngine.EventSystems;

public class MousePointer : MonoBehaviour
{
    private GameObject target = null;
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {            
            CastRay();
            if (target != null)
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    if (target.CompareTag("Door"))
                    {
                        Scene.Change_Scene(target.name);
                        GameObject.Find("마우스 포인터").GetComponent<AudioSource>().PlayOneShot(GameObject.Find("마우스 포인터").GetComponent<AudioSource>().clip);
                    }
                    if (target.CompareTag("Item"))
                    {
                        UI.Pick_Item(target);
                    }
                    if (target.CompareTag("Interacting"))
                    {
                        if (target.name.Equals("현미경"))
                        {
                            if (Player.Handle_Item == null || !Player.Handle_Item.Equals("테이프"))
                            {
                                Subtitles.Load_Subtitles("무언가가 필요한 것 같다.");
                                return;
                            }
                        }
                        if (target.name.Equals("복도_액자"))
                        {
                            if (GameObject.Find("배경").GetComponent<SpriteRenderer>().material.GetFloat("_Red") != 1f)
                            {
                                Subtitles.Load_Subtitles("흐릿해서 잘 보이지 않는다. 나중에 다시 봐야겠다.");
                                return;
                            }
                        }
                        if(target.name.Equals("식물표본 통"))
                        {
                            if(!Puzzle_Temperature.isClear)
                            {
                                Subtitles.Load_Subtitles("표본이 얼어있다.");
                                return;
                            }
                        }
                        Display.Interaction_target = target.name;
                        Player.isInteraction = true;
                    }
                }

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
}
