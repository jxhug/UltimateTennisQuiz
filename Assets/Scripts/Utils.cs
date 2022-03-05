using UnityEngine;

namespace UtilsNS
{
    public class Utils : MonoBehaviour
    {
        private static DeviceOrientation currentOrientation, lastOrientation;

        public Utils()
		{
            currentOrientation = lastOrientation = Input.deviceOrientation;
            Screen.orientation = ScreenOrientation.AutoRotation;
        }

        public bool UpdateOrientation(ref GameObject portrait, ref GameObject landscape)
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
