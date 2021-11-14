using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
	
	public GameObject EmptyObj;
	
	public GameObject ObjectToLook;
	
	public GameObject ArrowObject;
	
    // Start is called before the first frame update
    void Start()
    {
	    EmptyObj = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
	    ArrowObject.transform.LookAt(ObjectToLook.transform);
	    //ArrowObject.transform.localEulerAngles = new Vector3(ArrowObject.transform.localEulerAngles.x, ArrowObject.transform.localEulerAngles.y, EmptyObj.transform.localEulerAngles.z);
    }
}
