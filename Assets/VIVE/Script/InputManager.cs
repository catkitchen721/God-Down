using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager : MonoBehaviour {
	GameObject eventSystem, VREventSystem;

	void Awake()
	{
		try{
			eventSystem = GameObject.FindObjectOfType<UnityEngine.EventSystems.StandaloneInputModule>().gameObject;
		}
		catch(Exception e){
			eventSystem = new GameObject("EventSystem");
			eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
			eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
		}
		try{
			VREventSystem = GameObject.FindObjectOfType<UnityEngine.EventSystems.OVRInputModule>().gameObject;
		}
		catch(Exception e){
			VREventSystem = new GameObject("VREventSystem");
			VREventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
			VREventSystem.AddComponent<UnityEngine.EventSystems.OVRInputModule>();
			VREventSystem.GetComponent<UnityEngine.EventSystems.OVRInputModule>().controllerPointer = GameObject.FindObjectOfType<VIVEPointer>();
			VREventSystem.GetComponent<UnityEngine.EventSystems.OVRInputModule>().rayAnchor = GameObject.FindObjectOfType<VIVEPointer>().transform;
		}
	}

	void Start()
	{
		eventSystem.SetActive(false);
		VREventSystem.SetActive(true);
	}

	void FixedUpdate () {
		if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)){
			eventSystem.SetActive(true);
			VREventSystem.SetActive(false);
		}
		if(VIVEControllers.instance.R_triggerDown || VIVEControllers.instance.R_gripGrabDown || VIVEControllers.instance.R_trackpadDown
		|| VIVEControllers.instance.L_triggerDown || VIVEControllers.instance.L_gripGrabDown || VIVEControllers.instance.L_trackpadDown){
			eventSystem.SetActive(false);
			VREventSystem.SetActive(true);
		}
	}
}
