using UnityEngine;

public class DieObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public bool IsSelected { get; set; }
    public bool IsHoveredOver { get; set; }
    public Die Die { get; set; }

    [SerializeField] private GameObject dieHover;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        dieHover.SetActive(IsHoveredOver);
    }

    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

}
