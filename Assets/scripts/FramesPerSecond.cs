using System.Collections;
using UnityEngine;

public class FramesPerSecond : MonoBehaviour
{
  [SerializeField] private Color textColor = Color.red;

  float deltaTime = 0.0f;

  private Rect rect;
  private GUIStyle style;
 
  void Start() {
    rect = new Rect(50, 50, 400, 100);

    style = new GUIStyle();
    style.fontSize = 30;
    style.normal.textColor = textColor;
  }

  void Update()
  {
    deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
  }
 
  void OnGUI()
  {
    float msec = deltaTime * 1000.0f;
    float fps = 1.0f / deltaTime;
    string text = string.Format("FPS: {0:0.}", fps);
    GUI.Label(rect, text, style);
  }
}