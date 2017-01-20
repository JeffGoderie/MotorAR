using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Leap.Unity.InputModule {
  public class SliderVolume : MonoBehaviour {
    public AudioSource source;
    float volume = 0f;
    float currentValue = -1f;
    float previousValue = -1f;
    float maxValue = 0f;
    float TimeLastSlid = 0f;
    ColorBlock colorBlock;

    void Start() {
      maxValue = GetComponent<Slider>().maxValue;
      colorBlock = GetComponent<Slider>().colors;
    }

    void Update() {
      volume = Mathf.Lerp(volume, Mathf.Abs(currentValue - previousValue) * 40f, 0.4f);
      previousValue = currentValue;
      source.volume = volume;

      if (Time.time - TimeLastSlid > 0.5f) {
        source.Stop();
      } else if (!source.isPlaying) {
        source.Play();
      }
    }

    public void setSliderSoundVolume(float sliderposition) {
      Color normal = Color.HSVToRGB(sliderposition, 1.0f, 1.0f);
      Color pressed = Color.HSVToRGB(sliderposition, 1.0f, 0.8f);
      colorBlock.highlightedColor = normal;
      colorBlock.normalColor = normal;
      colorBlock.pressedColor = pressed;
      GetComponent<Slider>().colors = colorBlock;
      currentValue = sliderposition / maxValue;
      TimeLastSlid = Time.time;
    }
  }
}