using UnityEngine;
using UtilsNS;
using SettingsNS;


public class MainMenu : MonoBehaviour
{

    public GameObject portraitCanvas;
    public GameObject landscapeCanvas;

    private Utils utils;
    private Settings settings;


    void Awake()
    {
        utils.UpdateOrientation(ref portraitCanvas, ref landscapeCanvas);
        settings.LoadSettings();
    }

	private void Update()
	{
        utils.UpdateOrientation(ref portraitCanvas, ref landscapeCanvas);
    }
}
