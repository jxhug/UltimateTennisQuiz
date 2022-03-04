using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UtilsNS;
using SettingsNS;


public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private GameObject portraitCanvas;
    [SerializeField]
    private GameObject landscapeCanvas;

    private DeviceOrientantion currentOrientation, lastOrientation;

    private float sfxSliderValue;
    private float musicSliderValue;

    private Utils utils;
    private SettingsMenu settingsMenu;


    void Awake()
    {
        currentOrientation = lastOrientation = Input.deviceOrientation;
        Screen.orientation = ScreenOrientation.AutoRotation;
        utils.OrientationChanged(currentOrientation, ref portraitCanvas, ref landscapeCanvas);
        settingsMenu.LoadSettings();
    }

	private void Update()
	{
        currentOrientation = Input.deviceOrientation;
        if (currentOrientation != lastOrientation)
		{
            utils.OrientationChanged(currentOrientation, ref portraitCanvas, ref landscapeCanvas);
            lastOrientation = currentOrientation;
        }
    }
}
