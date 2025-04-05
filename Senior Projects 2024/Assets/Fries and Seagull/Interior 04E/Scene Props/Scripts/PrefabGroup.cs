using System.Collections.Generic;
using UnityEngine;

namespace Seagull.Interior_04E.SceneProps {
    public class PrefabGroup : MonoBehaviour {
        public List<GameObject> prefabs;
        public GameObject getRandomPrefab() {
            int r = Random.Range(0, prefabs.Count);
            return prefabs[r];
        }
    }
}