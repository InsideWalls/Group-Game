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
    void Awake()
    {

        hideui();
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
}
