using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Leap.Unity.InputModule {
  public class ToggleToggler : MonoBehaviour {
    public UnityEngine.UI.Image image;
    public Color OnColor;
    public Color OffColor;

    public void SetToggle(Toggle toggle) {
      if (toggle.isOn) {
        image.color = OnColor;
      } else {
        image.color = OffColor;
      }
    }
  }
}