using UnityEngine;

namespace UtilsNS
{
    public class Utils : MonoBehaviour
    {
        public void OrientationChanged(DeviceOrientation orientation, ref GameObject portrait, ref GameObject landscape)
        {
            if (orientation == DeviceOrientation.Portrait)
            {
                portrait.SetActive(true);
                landscape.SetActive(false);
            }
            if (orientation == DeviceOrientation.LandscapeRight || orientation == DeviceOrientation.LandscapeLeft)
            {
                portrait.SetActive(false);
                landscape.SetActive(true);
            }
        }
    }
}
