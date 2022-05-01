using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UtilsNS;

public class Store : MonoBehaviour
{
    public GameObject parentPortraitCanvas;
    public GameObject parentLandscapeCanvas;

    private Utils utils;

    private void Start()
    {
        utils = new Utils();
        utils.CheckIfOrientationUpdated(parentPortraitCanvas, parentLandscapeCanvas, true);
    }

    private void Update()
    {
        utils.CheckIfOrientationUpdated(parentPortraitCanvas, parentLandscapeCanvas, false);
    }
}
