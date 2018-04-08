using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderFix : MonoBehaviour {

	Shader standardShader;

	void Start() {
		standardShader = Shader.Find("Standard");
	}

	public void changeShader() // because shadow for assetbundle is cucked.
	{
		var renderers = FindObjectsOfType<Renderer>() as Renderer[];
		for (int i = 0; i < renderers.Length; i++)
			renderers[i].material.shader = standardShader;
	}
}
