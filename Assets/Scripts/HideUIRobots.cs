using UnityEngine;

public class HideUIRobots : MonoBehaviour
{
    public GameObject robot1;
    public GameObject robot2;
    public GameObject robot3;
    public GameObject robot4;
    public GameObject robot5;
    public GameObject robot6;
    public GameObject robot7;


    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;
    public GameObject circle4;
    public GameObject circle5;
    public GameObject circle6;
    public GameObject circle7;
    void Awake()
    {

        hideui();
        hidecircle();
    }
    public void hideui()
    {
        robot1.SetActive(false);
        robot2.SetActive(false);
        robot3.SetActive(false);
        robot4.SetActive(false);
        robot5.SetActive(false);
        robot6.SetActive(false);
        robot7.SetActive(false);
    }
    public void hidecircle()
    {
        circle1.SetActive(false);
        circle2.SetActive(false);
        circle3.SetActive(false);
        circle4.SetActive(false);
        circle5.SetActive(false);
        circle6.SetActive(false);
        circle7.SetActive(false);
    }
}
