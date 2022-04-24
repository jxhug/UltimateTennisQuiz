using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSlider : MonoBehaviour
{
    private Toggle toggle;

	private Animator animator;

	[SerializeField]
	private GameObject toggleSwitch;

	[SerializeField]
	private Image toggleBack;


	private void Awake()
	{
		toggle = gameObject.GetComponent<Toggle>();
		animator = gameObject.GetComponent<Animator>();
		animator.enabled = false;

		if (toggle.isOn)
		{
			toggleBack.color = new Color32(76, 217, 100, 255);
			toggleSwitch.GetComponent<RectTransform>().anchoredPosition = new Vector2(50f, 0f);
		}
		else
		{
			toggleBack.color = new Color32(204, 204, 204, 255);
			toggleSwitch.GetComponent<RectTransform>().anchoredPosition = new Vector2(-47.5f, 0f);
		}
	}

	public void SliderToggle()
	{
		if (toggle.isOn)
		{
			StartCoroutine(EnableToggle());
		}
		else
		{
			StartCoroutine(DisableToggle());
		}
	}

	IEnumerator EnableToggle()
	{
		animator.enabled = true;
		animator.Play("EnableToggle");
		yield return new WaitForSeconds(.5f);
		animator.enabled = false;
		toggleBack.color = new Color32(76, 217, 100, 255);
		toggleSwitch.GetComponent<RectTransform>().anchoredPosition = new Vector2(50f, 0f);
	}

	IEnumerator DisableToggle()
	{
		animator.enabled = true;
		animator.Play("DisableToggle");
		yield return new WaitForSeconds(.5f);
		animator.enabled = false;
		toggleBack.color = new Color32(204, 204, 204, 255);
		toggleSwitch.GetComponent<RectTransform>().anchoredPosition = new Vector2(-47.5f, 0f);
	}
}


