using UnityEngine;

public class HardModeModifiers : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToDestroy;

    private void Start()
    {
        // Delay to check for hard mode
        Invoke("CheckHardMode", 0.1f);
    }

    //private void CheckHardMode()
    //{
    //    if (GameDifficultyManager.Instance.HardMode)
    //    {
    //        DestroyObjects();
    //    }}

    private void DestroyObjects()
    {
        // Destroy the objects
        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null)
            {
                Destroy(obj);
            }}}}