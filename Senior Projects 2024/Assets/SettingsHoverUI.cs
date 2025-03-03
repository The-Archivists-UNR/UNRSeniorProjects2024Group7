using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;



public class SettingsHoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    Vector3 cachedScale;
    Vector3 cachedPos;
    bool clicked = false;
    void Start() {
        cachedScale = transform.localScale;
        cachedPos = transform.localPosition;
    }

    public void OnPointerEnter(PointerEventData eventData) {

        transform.localScale = new Vector3(1.15f, 1.15f, 1.15f);
        // transform.localPosition = new Vector3(1f, 1f, 1.25f);
    }

    public void OnPointerExit(PointerEventData eventData) {

        transform.localScale = cachedScale;
    }

}
