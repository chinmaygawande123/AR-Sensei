using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainsFollower : MonoBehaviour
{
	public Transform Target;

    // Update is called once per frame
    void Update()
	{
		transform.Rotate(0,0.5f,0);
	    transform.position = Target.position;
    }
}
