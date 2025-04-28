using UnityEngine;
using UnityEngine.UI;

public class ButtonListner : MonoBehaviour
{
    [SerializeField] private Button HardOnButton;
    [SerializeField] private Button HardOffButton;

    private void Start()
    {
        //Make the buttons send data on click
        if (HardOnButton != null)
            HardOnButton.onClick.AddListener(() => GameDifficultyManager.Instance.EnableHardMode());

        if (HardOffButton != null)
            HardOffButton.onClick.AddListener(() => GameDifficultyManager.Instance.DisableHardMode());
    }
}