using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCubeEveryFrame : MonoBehaviour
{
	public Transform transform;
	public Transform FollowTransform;
	Vector3 lastPos;
	Vector3 newPos;
	float t;

	void Start()
	{
		lastPos = transform.eulerAngles;
		NewAngle();
	}

	void Update()
	{
		transform.eulerAngles = Vector3.Lerp(lastPos, newPos, t);
		transform.position = new Vector3(FollowTransform.position.x, FollowTransform.position.y, FollowTransform.position.z);
		t+=0.01f;
		if(t>3)
			NewAngle();
	}

	void NewAngle()
	{		
		lastPos = newPos;
		newPos = new Vector3(
			Random.Range(0f, 360f),
			Random.Range(0f, 360f),
			Random.Range(0f, 360f));
		t = 0;
	}
}
