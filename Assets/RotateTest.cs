using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTest : MonoBehaviour
{

	public Transform Pivot;
	private RaycastHit _hit;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0))
		{
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(r, out _hit))
			{
				Vector3 p = _hit.point;
				p.y = Pivot.position.y;
				Vector3 dir = (p - Pivot.position).normalized;

				Pivot.transform.rotation = Quaternion.LookRotation(dir);

				Debug.DrawLine(p, Pivot.position, Color.blue, 1.0f);
			}
		}
	}
}
