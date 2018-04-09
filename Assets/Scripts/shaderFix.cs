using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderFix : MonoBehaviour {

	Shader standardShader;
	public void changeShader() // because shadow for assetbundle is cucked.
	{
		var renderers = FindObjectsOfType<Renderer>() as Renderer[];
		for (int i = 0; i < renderers.Length; i++)
		{
			standardShader = renderers[i].material.shader;
			renderers[i].material.shader = standardShader;
		}
	}
}
