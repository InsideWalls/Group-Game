using UnityEngine;

public class GameDifficultyManager : MonoBehaviour
{
    //Make it stay across scenes
    public static GameDifficultyManager Instance { get; private set; }

    private bool _hardMode = false;
    public bool HardMode
    {
        get => _hardMode;
        private set
        {
            _hardMode = value;
            PlayerPrefs.SetInt("HardMode", _hardMode ? 1 : 0);
            PlayerPrefs.Save();
            UpdateHealth();
        }}

    //Health numbers
    private int _currentHealth;
    public int CurrentHealth
    {
        get => _currentHealth;
        private set
        {
            _currentHealth = value;
            //Only triggers on health change and there currently are no ways to lose health
            //Debug.Log($"Health set to: {_currentHealth}");
        }}

    private void Awake()
    {
        //Only happen once
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            //Load difficulty setting or defualt to normal
            HardMode = PlayerPrefs.GetInt("HardMode", 0) == 1;
        }
        else
        {
            Destroy(gameObject);
        }}

    //Buttons that enable or disable hard mode
    public void EnableHardMode()
    {
        HardMode = true;
        Debug.Log("Hard Mode: ON");
    }

    public void DisableHardMode()
    {
        HardMode = false;
        Debug.Log("Hard Mode: OFF");
    }

    public void ToggleHardMode()
    {
        HardMode = !HardMode;
        Debug.Log($"Hard Mode: {(HardMode ? "ON" : "OFF")}");
    }
    //If hard mode is off then 5 health. If hard mode is on then 3 health
    private void UpdateHealth()
    {
        CurrentHealth = HardMode ? 3 : 5;
    }}