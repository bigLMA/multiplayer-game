using Misc;
using UnityEngine;
using UnityEngine.UI;


namespace UI.MainMenu
{
    public class QuitButton : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                GameManager.instance.Quit();
            });
        }
    }
}
