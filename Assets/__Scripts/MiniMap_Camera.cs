using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Camera))]
public class MiniMap_Camera : MonoBehaviour {
	[Header("Inscribed")]
	public float camSize=0;
	public LayerMask layersToZoomTo;
	public float zoomBorder = 0.1f;
	private Camera cam;

	void Start () {
		cam = GetComponent<Camera> ();
		if (camSize == 0) {
			FillCameraWithLevel ();
		}
	}

	private void FillCameraWithLevel()
	{
		if(!cam.orthographic){
			cam.orthographic = true;
		}
		GameObject[] allGOs = FindObjectsOfType<GameObject> ();
		Vector3 xzMin = Vector3.zero, xzMax = Vector3.zero;
		Renderer tRend;
		int binaryZoomLayers = layersToZoomTo.value;
		int binaryLayer;
		bool foundSomethingToZoomTo = false;

		for (int i = 0; i < allGOs.Length; i++) {
			binaryLayer = 1 << allGOs [i].layer;
			if ((binaryLayer & binaryZoomLayers) == binaryLayer) {
				foundSomethingToZoomTo = true;
				tRend = allGOs [i].GetComponent<Renderer> ();
				if (tRend != null) {
					xzMin.x = Mathf.Min (xzMin.x, tRend.bounds.min.x);
					xzMin.z = Mathf.Min (xzMin.z, tRend.bounds.min.z);
					xzMax.x = Mathf.Max (xzMax.x, tRend.bounds.max.x);
					xzMax.z = Mathf.Max (xzMax.z, tRend.bounds.max.z);
				}
			}
		}

		gameObject.SetActive (foundSomethingToZoomTo);
		if (!foundSomethingToZoomTo)
			return;
		Vector3 center = (xzMin + xzMax)/2f;

		#if DEBUG_MiniMap_Camera_ShowMinMax
		DEBUG.DrawLine(xzMin, center, Color.magenta, 60);
		DEBUG.DrawLine(xzMax, center, Color.magenta, 60);
		#endif

		center.y = 16;
		transform.position = center;

		float ratioHW = (cam.rect.height * Screen.height) / (cam.rect.width * Screen.width);
		float width = xzMax.x - xzMin.x;
		float height = xzMax.z - xzMin.z;
		float size = Mathf.Max (width * ratioHW, height) * 0.5f;
		size *= 1 + zoomBorder;
		cam.orthographicSize = size;
	}
}
