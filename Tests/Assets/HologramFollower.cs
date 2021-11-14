using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HologramFollower : MonoBehaviour
{
	
	public Transform FollowTransform;
	
	public ARTapToPlaceObject PlacementIndicatorObject; 
	
	public int StartExperience;
	
	public MeshRenderer BrainMesh;
	
	public Material ColoredMaterial;
	
	public Transform BrainBig;
	
	public Camera ARCamera;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
	public Collider collider;
	public float elevation;
	public float cameraDistance = 2.0f;

    // Update is called once per frame
    void Update()
	{
		transform.Rotate(0,0.5f,0);
		
		if(StartExperience == 1)
		{
			PlacementIndicatorObject.enabled = false;
			BrainMesh.material = ColoredMaterial;			
			transform.localScale = Vector3.Lerp(transform.localScale, BrainBig.localScale, 0.1f);
		}
		
		else
		{
			transform.position = FollowTransform.position;
		}
    }
}
