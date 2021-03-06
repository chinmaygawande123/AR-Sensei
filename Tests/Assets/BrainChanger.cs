using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class BrainChanger : MonoBehaviourPunCallbacks
{
	public Material MainMaterial;
	
	public Material TransperentMaterial;
	
	public MeshRenderer BrainRenderer;
	
	public int PartChanger;
	
	public List<Transform> AllObjectsToAppear;
	
	public GameObject OragnsHolder;
	
	public RectTransform MainObject;
	
	public int MultilplyerPostition;
	
	public GameObject LeftArrowObject;
	
	public GameObject RightArrowObject;
	
	public List<Transform> AllObjectsText;
	
	public TextMeshProUGUI Title;
	
	public TextMeshProUGUI MainInformation;
	
	public UIHandler UIHandler;
		
	public MeshRenderer[] DigestiveSystemMeshs; 
	
	public Material[] DigestiveSystemNormalMaterials;
	
	public Material[] DigestiveSystemTransperentMaterials;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		foreach (Transform child in OragnsHolder.transform)
		{
			AllObjectsToAppear.Add(child);
		}
		
		foreach (Transform child in transform)
		{
			AllObjectsText.Add(child);
		}
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		if(PartChanger != 0)
		{
			if(this.transform.parent.name != "Digestive System Canvas")
			{
				BrainRenderer.material = TransperentMaterial;
			}
			
			else
			{
				for(int i = 0; i < DigestiveSystemMeshs.Length; i++)
				{
					if(i == ( PartChanger - 1))
					{
						print(DigestiveSystemMeshs[i].name);
						DigestiveSystemMeshs[i].material = DigestiveSystemNormalMaterials[i];
					}
					
					else
					{
						DigestiveSystemMeshs[i].material = DigestiveSystemTransperentMaterials[i];
					}
				}
			}
		}
		
		else
		{
			RightArrowObject.SetActive(true);
			Title.text = "Information Extender";
			MainInformation.text = "By clicking this arrow you will be able to see and hear Microsoft Azure’s Neural Text-to-Speech voice about the selected organ. This voice is Generated by this Native Bridge of Unity to Microsoft Cloud, and it happens just by clicking the up arrow button. You should realy try it. Your and others voice will be muted and the voice will be streamed throughout the server and its clients.";
			BrainRenderer.material = MainMaterial;
		}
		
		if(PartChanger > 0)
		{
			
			LeftArrowObject.SetActive(true);
						
			for(int i = 0; i < AllObjectsToAppear.Count; i++)
			{
								
				if(AllObjectsToAppear[i] == AllObjectsToAppear[PartChanger - 1])
				{
					MainInformation.text = UIHandler.ArrayInformation[i];
					AllObjectsToAppear[i].gameObject.SetActive(true);
					AllObjectsText[i+1].GetChild(0).GetComponent<TextMeshProUGUI>().text = AllObjectsToAppear[i].gameObject.name;
					Title.text = AllObjectsToAppear[i].gameObject.name; 
				}
				
				else
				{
					if(this.transform.parent.name != "Digestive System Canvas")
					{
						AllObjectsToAppear[i].gameObject.SetActive(false);
					}
				}
			}
		}
		
		else
		{
			LeftArrowObject.SetActive(false);
		}
		
		if(PartChanger == UIHandler.ArrayInformation.Length)
		{
			RightArrowObject.SetActive(false);
		}
		
		MainObject.localPosition = Vector3.Lerp(MainObject.localPosition, new Vector3(-1440 * PartChanger,MainObject.localPosition.y,0), 0.1f);
		
	}
	
	public void ChangeAdd()
	{
		photonView.RPC("ServerChangeAdd", RpcTarget.All);
	}
	
	[PunRPC]
	void ServerChangeAdd()
	{
		PartChanger++;
	}
	
	public void ChangeSubs()
	{
		photonView.RPC("ServerChangeSubs", RpcTarget.All);
	}
	
	[PunRPC]
	void ServerChangeSubs()
	{
		PartChanger--;
	}
}
