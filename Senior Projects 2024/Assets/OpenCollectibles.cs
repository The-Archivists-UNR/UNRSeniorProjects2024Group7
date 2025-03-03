using UnityEngine;
//author: Kat
//taking code from fenn's book switch 
public class OpenCollectibles : MonoBehaviour
{
    [SerializeField] private GameObject Collectibles;

    private void OnMouseUpAsButton()
    {
        Collectibles.SetActive(true);
    }
}