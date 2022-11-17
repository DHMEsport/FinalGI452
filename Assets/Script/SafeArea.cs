using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    public Canvas canvas;
    RectTransform panelSafeArea;
     
     
     
    Rect CurrentSafeArea = new Rect();
    ScreenOrientation CurrentOrientation = ScreenOrientation.AutoRotation;
    // Start is called before the first frame update
    void Start()
    {
        panelSafeArea = GetComponent<RectTransform>();
     
     
     
        CurrentOrientation = Screen.orientation;
        CurrentSafeArea = Screen.safeArea;
    }
     
     
     
    // Update is called once per frame
     
     
     
     
    void ApplySafeArea()
    {
        if(panelSafeArea == null)
        {
            return;
        }
        Rect SafeArea = Screen.safeArea;  
     
     
     
        Vector2 anchorMin = SafeArea.position;
        Vector2 anchorMax = SafeArea.position + SafeArea.size;
     
     
     
        anchorMin.x /= canvas.pixelRect.width;
        anchorMin.y /= canvas.pixelRect.height;
     
     
     
     
        anchorMax.x /= canvas.pixelRect.width;
        anchorMax.y /= canvas.pixelRect.height;
     
     
     
        panelSafeArea.anchorMin = anchorMin;
        panelSafeArea.anchorMax = anchorMax;
        CurrentOrientation = Screen.orientation;
        CurrentSafeArea = Screen.safeArea;
     
     
     
    }
    void Update()
    {
        if((CurrentOrientation != Screen.orientation)  || (CurrentSafeArea != Screen.safeArea))
        {
            ApplySafeArea();
        }
    }
}
