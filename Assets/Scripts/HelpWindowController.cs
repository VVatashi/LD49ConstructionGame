using UnityEngine;

public class HelpWindowController : MonoBehaviour
{
    public GameObject HelpWindow;

    public void OnCloseClick()
    {
        HelpWindow.SetActive(false);
    }
}
