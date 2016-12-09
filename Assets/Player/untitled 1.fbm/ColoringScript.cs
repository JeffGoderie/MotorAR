using UnityEngine;
using System.Collections;
using System;

public class ColoringScript : MonoBehaviour {

	public Material unselected;
	public Material selected;
	[SerializeField]
	private float gradientStepSize = 0.10f;

	private string floatRangeProperty = "Pattern Specific";
	private string colorProperty = "Output_Color";
    private float progress;

	private Renderer renderer;

	private bool started = false;

	void Start(){
		renderer = GetComponent<Renderer>();
		progress = 0.25f;
	}

	public void color(Color color){
		Debug.Log("4");
		Debug.Log(renderer.sharedMaterial);
		foreach(Material m in renderer.sharedMaterials){
			ProceduralMaterial substance = m as ProceduralMaterial;
			if (substance) {
				progress = Math.Min(gradientStepSize + progress,1f);
	            substance.SetProceduralFloat(floatRangeProperty, progress);
				//if(!started){
					substance.SetProceduralColor(colorProperty, color);
					//started = true;
				//}        
	            substance.RebuildTextures();
	            break;
        	}
		}
	}

	public void setSelected(){
		setSelectionMaterial(unselected, selected);
	}

	public void setDeselected(){
		setSelectionMaterial(selected, unselected);
	}

	private void setSelectionMaterial(Material s, Material n){
		Material[] newMaterials = new Material[renderer.sharedMaterials.Length];

		for(int i = 0; i < renderer.sharedMaterials.Length; i++){
			Material m = renderer.sharedMaterials[i];
			if (m == s){
				newMaterials[i] = n;
			}
			else{
				newMaterials[i] = m;
			}
		}

		renderer.sharedMaterials = newMaterials;
	}
}
