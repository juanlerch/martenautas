using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LabsButtonUtil : MonoBehaviour {

	public GameObject OnEnter = null;
	public GameObject OnClick = null;

	AudioSource onEnter;
	AudioSource onClick;

	bool interactable=true;

	// mouseEnter mouseClick
	// Use this for initialization
	void Start () {
		Button btn = GetComponent<Button>();
		if(btn!=null) interactable = btn.interactable;
		if (btn != null)
			btn.onClick.AddListener (TaskOnClick);
		else {
			Toggle toggle = GetComponent<Toggle >();
			if (toggle != null) { 
				toggle.onValueChanged.AddListener (delegate {
					ToggleValueChanged(toggle);
				});
			}
		} 

		this.gameObject.AddComponent<EventTrigger>();
		EventTrigger trigger = GetComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerEnter;
		entry.callback.AddListener((data) => { OnPointerEnterDelegate((PointerEventData)data); });
		trigger.triggers.Add(entry);

		if (OnEnter != null)
		{
			onEnter = OnEnter.GetComponent<AudioSource>();
		}
		else {
			GameObject temp = GameObject.FindGameObjectWithTag("buttonOnEnterAudio");
			if (temp != null) { 
				onEnter = temp.GetComponent<AudioSource>();
			}
		}

		if (OnClick != null)
		{
			onClick= OnClick.GetComponent<AudioSource>();
		}
		else {
			GameObject temp = GameObject.FindGameObjectWithTag("buttonOnClickAudio");
			if (temp != null) { 
				onClick = temp.GetComponent<AudioSource>();
			}
		}

		/**/
	}

	public void OnPointerEnterDelegate(PointerEventData data)
	{
		if(onEnter!=null && interactable) onEnter.Play();
	}


	void TaskOnClick()
	{
		if (onClick != null && interactable ) onClick.Play();
	}

	//Output the new state of the Toggle into Text
	void ToggleValueChanged(Toggle change)
	{
		if (onClick != null && interactable) onClick.Play();
	}


	void TaskOnEnter()
	{
		// audioOnEnter.Play();
	}
	// Update is called once per frame
	void Update () {

	}
}