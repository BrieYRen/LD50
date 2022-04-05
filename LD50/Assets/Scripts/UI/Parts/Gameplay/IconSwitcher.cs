using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IconSwitcher : MonoBehaviour
{
    public Sprite[] sprites;
    int index = 0;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.sprite = sprites[index];
    }

    public void NextImage()
    {
        if (index == sprites.Length - 1)
            index = 0;
        else
            index++;

        image.sprite = sprites[index];
    }
}
