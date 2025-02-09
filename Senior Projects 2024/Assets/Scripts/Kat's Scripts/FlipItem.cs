using UnityEngine;
using DG.Tweening;

public class FlipItem : MonoBehaviour
{
    private bool tagFlipped = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1)) Flip();
    }

    private void Flip()
    {
        tagFlipped = !tagFlipped;
        transform.DORotate(new(0, tagFlipped? 0f : 180f, 0), 0.25f);
    }
}
