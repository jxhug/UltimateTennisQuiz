using UnityEngine;

namespace UtilsNS
{
    public class Utils
    {
        public static DeviceOrientation currentOrientation, lastOrientation;

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
                SetActiveOrientation(portrait, landscape);
                lastOrientation = currentOrientation;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetActiveOrientation(GameObject portrait, GameObject landscape)
		{
            if (currentOrientation == DeviceOrientation.Portrait)
            {
                portrait.SetActive(true);
                landscape.SetActive(false);
            }
            else if (currentOrientation == DeviceOrientation.LandscapeLeft || currentOrientation == DeviceOrientation.LandscapeRight)
            {
                portrait.SetActive(false);
                landscape.SetActive(true);
            }
			else
			{
                // If not portrait or landscape (e.g. face-up or face-down) then default to the last known orientation
                if (lastOrientation == DeviceOrientation.Portrait)
                {
                    portrait.SetActive(true);
                    landscape.SetActive(false);
                }
                else
                {
                    portrait.SetActive(false);
                    landscape.SetActive(true);
                }
            }
        }
    }
}
