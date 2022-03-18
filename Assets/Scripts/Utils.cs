using UnityEngine;

namespace UtilsNS
{
    public class Utils
    {
        public static ScreenOrientation currentOrientation, lastOrientation;

        public Utils()
		{
            currentOrientation = Screen.orientation;
            lastOrientation = ScreenOrientation.Portrait;
            //Screen.orientation = ScreenOrientation.AutoRotation;
        }

        public bool CheckIfOrientationUpdated(GameObject portrait, GameObject landscape, bool forceUpdate)
        {
            currentOrientation = Screen.orientation;
            if ((currentOrientation != lastOrientation) || forceUpdate)
            {
                if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.tvOS || Application.platform == RuntimePlatform.OSXEditor)
				{
                    landscape.SetActive(true);
                    return true;
				}
                else
				{
                    if (currentOrientation == ScreenOrientation.Portrait || currentOrientation == ScreenOrientation.PortraitUpsideDown)
                    {
                        portrait.SetActive(true);
                        landscape.SetActive(false);
                    }
                    else
                    {
                        portrait.SetActive(false);
                        landscape.SetActive(true);
                    }

                    lastOrientation = currentOrientation;
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
