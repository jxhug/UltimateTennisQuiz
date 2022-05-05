using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour
{
    private string bigthreequestionpack = "com.jam3shug.ultimatetennisquiz.bigthreequestionpack";

    [SerializeField]
    private GameObject restorePuchasesButton;

	private void Awake()
	{
		if (Application.platform != RuntimePlatform.IPhonePlayer)
		{
            restorePuchasesButton.SetActive(false);
		}
	}

	public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id == bigthreequestionpack)
		{
            Debug.Log("big three quesions purchased");
		}
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError(product.definition.id + " failed because " + failureReason);
    }
}
