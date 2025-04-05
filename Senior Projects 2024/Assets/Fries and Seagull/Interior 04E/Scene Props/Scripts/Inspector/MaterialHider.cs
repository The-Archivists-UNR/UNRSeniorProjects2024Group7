#if UNITY_EDITOR
using System;
using UnityEngine;

namespace Seagull.Interior_04E.Inspector {
    [ExecuteInEditMode]
    public class MaterialHider : MonoBehaviour {
        public bool hided = true;
        
        private void OnValidate() {
            foreach (var material in gameObject.GetComponent<Renderer>().sharedMaterials) {
                if (hided)
                    material.hideFlags |= HideFlags.HideInInspector;
                else
                    material.hideFlags &= ~HideFlags.HideInInspector;
            }
        }
    }
}

#endif