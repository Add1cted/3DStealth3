using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Renderer))]
public class MiniMap_Blip : MonoBehaviour {
	public Transform trackedTransform;
	public Color color = Color.white;
	public float yHeight = 5;

	Renderer rend;

	void Start () {
		rend = GetComponent<Renderer> ();
		rend.material.color = color;
	}

	void FixedUpdate () {
		transform.position = trackedTransform.position + Vector3.up * yHeight;
		transform.rotation = trackedTransform.rotation;	
	}
}
