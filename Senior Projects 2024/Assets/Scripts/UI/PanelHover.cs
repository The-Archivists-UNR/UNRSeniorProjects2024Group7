using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIOnHoverEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    Vector3 cachedScale;

    void Start() {

        cachedScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData) {

        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void OnPointerExit(PointerEventData eventData) {

        transform.localScale = cachedScale;
    }
}
