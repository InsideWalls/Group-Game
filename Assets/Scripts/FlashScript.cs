using UnityEngine;
using UnityEngine.UI;

public class FlashScript : MonoBehaviour
{
    public Image flash;
    private Color flashColor;
    private bool flashed;
    public float flashLength = 0.6f;
    private float flashTime;

    void Start()
    {
        flashed = false;
        flashColor = new Color(flash.color.r, flash.color.g, flash.color.b, 1f);

        //flashbang();
    }

    void Update()
    {
        if (flashed)
        {
            flash.color = new Color(flash.color.r, flash.color.g, flash.color.b, (flashTime/flashLength)*0.8f);
            flashTime -=Time.deltaTime;
            if (flashTime <= 0)
            {
                flashed=false;
            }
        }
    }

    public void flashbang()
    {
        flashed = true;
        flash.color = flashColor;
        flashTime = flashLength;
    }
}
