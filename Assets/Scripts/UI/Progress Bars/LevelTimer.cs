using Level;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    private Image image;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = EndLevel.instance.levelDurationSec / (EndLevel.instance.levelDurationMins * 60);
    }
}
