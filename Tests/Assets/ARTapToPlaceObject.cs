using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{
	public GameObject objectToPlace;
	public GameObject placementIndicator;

	private ARSessionOrigin arOrigin;
	public ARRaycastManager arRayCast;
	private Pose placementPose;
	private bool placementPoseIsValid = false;

	void Start()
	{
		arOrigin = FindObjectOfType<ARSessionOrigin>();
	}

	void Update()
	{
		UpdatePlacementPose();
		UpdatePlacementIndicator();

		if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			PlaceObject();
		}
	}

	public HologramFollower ObjectManager; 
	public GameObject MainARUICanvas;

	private void PlaceObject()
	{
		ObjectManager.gameObject.SetActive(true);
		ObjectManager.StartExperience = 1;
		Destroy(placementIndicator.GetComponent<Animator>());
		MainARUICanvas.SetActive(true);
		//Instantiate(objectToPlace, placementPose.position, placementPose.rotation);
	}
	
	public GameObject CanvasARUI;
	
	public GameObject ScanSprite;

	private void UpdatePlacementIndicator()
	{
		if (placementPoseIsValid)
		{
			placementIndicator.SetActive(true);
			CanvasARUI.SetActive(false);
			ScanSprite.SetActive(false);
			placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
		}
		else
		{
			ScanSprite.SetActive(true);
			CanvasARUI.SetActive(true);
			placementIndicator.SetActive(false);
		}
	}

	private void UpdatePlacementPose()
	{
		
		#if !UNITY_EDITOR
		
		var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
		var hits = new List<ARRaycastHit>();
		arRayCast.Raycast(screenCenter, hits, TrackableType.Planes);

		placementPoseIsValid = hits.Count > 0;
		if (placementPoseIsValid)
		{
			placementPose = hits[0].pose;

			var cameraForward = Camera.current.transform.forward;
			var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
			placementPose.rotation = Quaternion.LookRotation(cameraBearing);
		}
		
		#endif
	}
}