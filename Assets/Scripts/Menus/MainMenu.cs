using UnityEngine;
using UnityEngine.SceneManagement;
using SettingsNS;
using UtilsNS;


public class MainMenu : MonoBehaviour
{

    public GameObject portraitCanvas;
    public GameObject landscapeCanvas;

    [SerializeField]
    private AudioMixer sfxMixer;
    [SerializeField]
    private AudioMixer musicMixer;

    private Utils utils;


    private void Start()
    {
        utils = new Utils();
        utils.UpdateOrientation(portraitCanvas, landscapeCanvas);

    }

	private void Update()
    {
        utils.UpdateOrientation(portraitCanvas, landscapeCanvas);
    }

}
