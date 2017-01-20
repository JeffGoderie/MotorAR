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
	private Color currentColor = Color.green;
    private float progress;
    private bool finished;

	private Renderer _renderer;

	void Start(){
		_renderer = GetComponent<Renderer>();
		progress = 0.25f;
		finished = false;
	}

	public Color getColor(){
		return currentColor;
	}

	public void resetColor(){
		if(_renderer){
			foreach(Material m in _renderer.sharedMaterials){
				ProceduralMaterial substance = m as ProceduralMaterial;
				if (substance) {
		            substance.SetProceduralFloat(floatRangeProperty, 0.25f);      
		            substance.RebuildTextures();
		            break;
	        	}
			}
			finished = false;
		}		
	}

	public void colorIn(Color color){
		foreach(Material m in _renderer.sharedMaterials){
			ProceduralMaterial substance = m as ProceduralMaterial;
			if (substance) {
				if(color != currentColor && !finished){
					currentColor = color;
					substance.SetProceduralColor(colorProperty, currentColor);
					progress = 0.25f;
				}
				progress = Math.Min(gradientStepSize + progress,1f);
				if(progress == 1.0f){
					finished = true;
				}
	            substance.SetProceduralFloat(floatRangeProperty, progress);      
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
		Material[] newMaterials = new Material[_renderer.sharedMaterials.Length];

		for(int i = 0; i < _renderer.sharedMaterials.Length; i++){
			Material m = _renderer.sharedMaterials[i];
			if (m == s){
				newMaterials[i] = n;
			}
			else{
				newMaterials[i] = m;
			}
		}

		_renderer.sharedMaterials = newMaterials;
	}
}
