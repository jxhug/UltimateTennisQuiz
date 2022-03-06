using UnityEngine;

namespace UtilsNS
{
    public class Utils
    {
        private static DeviceOrientation currentOrientation, lastOrientation;

        public Utils()
		{
            currentOrientation = Input.deviceOrientation;
            lastOrientation = DeviceOrientation.Unknown;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

        public bool UpdateOrientation(GameObject portrait, GameObject landscape)
        {
            currentOrientation = Input.deviceOrientation;
            if (currentOrientation != lastOrientation)
            {
                if (currentOrientation == DeviceOrientation.Portrait)
                {
                    portrait.SetActive(true);
                    landscape.SetActive(false);
                }
                if (currentOrientation == DeviceOrientation.LandscapeRight || currentOrientation == DeviceOrientation.LandscapeLeft)
                {
                    portrait.SetActive(false);
                    landscape.SetActive(true);
                }

                lastOrientation = currentOrientation;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
