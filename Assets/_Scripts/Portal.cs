﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Portal : MonoBehaviour {

	// Use this for initialization
	public Material[] materials;
	public Transform device;
	bool wasInFront;
	bool inotherworld;
	void Start () {
		SetMaterials(false);
	}
	bool GetIsInFront()
	{
		Vector3 pos = transform.InverseTransformPoint(device.position);
		return pos.z >= 0 ? true : false;
	}
	void SetMaterials(bool fullRender)
	{
		var stencilTest = fullRender ? CompareFunction.NotEqual : CompareFunction.Equal;
		foreach (var mat in materials)
		{
			mat.SetInt("_StencilTest", (int)stencilTest);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.transform != device)
		{
			return;
		}
		wasInFront = GetIsInFront();
	}
	void OnTriggerStay(Collider other)
	{
		if(other.transform != device)
		{
			return;
		}
		bool isInFront = GetIsInFront();
		if((isInFront && !wasInFront)|| (wasInFront && !isInFront))
		{
			inotherworld = !inotherworld;
			SetMaterials(inotherworld);
		}
		wasInFront = isInFront;
	}

	void OnDestroy()
	{
		SetMaterials(true);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
