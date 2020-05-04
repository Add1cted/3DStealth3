using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap_Manager : MonoBehaviour {
	[Header("Inscribed")]
	public List <TrackTagInfo> tagsToTrack;
	private List <MiniMap_Blip> activeBlips = new List<MiniMap_Blip>();

	void Start () {
		AssignBlips ();
	}

	void AssignBlips(){
		if (activeBlips != null && activeBlips.Count > 0) {
			DestroyActiveBlips ();
		}
		GameObject blipGO;
		MiniMap_Blip blip;
		foreach (TrackTagInfo tC in tagsToTrack) {
			GameObject[] tGOs = GameObject.FindGameObjectsWithTag (tC.tag);
			if (tGOs.Length == 0) {
				continue;
			}
			foreach (GameObject tGO in tGOs) {
				blipGO = Instantiate<GameObject> (tC.prefab);
				blip = blipGO.GetComponent<MiniMap_Blip> ();
				blip.transform.SetParent (transform.parent);
				blip.color = tC.color;
				blip.trackedTransform = tGO.transform;
			}
		}

	}

	private void DestroyActiveBlips()
	{
		MiniMap_Blip blip;
		while (activeBlips.Count > 0) {	
			blip = activeBlips [activeBlips.Count - 1];
			activeBlips.Remove (blip);
			Destroy (blip.gameObject);
		}	
	}

	[System.Serializable]
	public struct TrackTagInfo{
		public string tag;
		public Color color;
		public GameObject prefab;
	}
}
