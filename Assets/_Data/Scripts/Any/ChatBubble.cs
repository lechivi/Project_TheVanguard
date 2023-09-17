using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatBubble : SaiMonoBehaviour
{
    [SerializeField] private Vector2 padding = new Vector2(4f, 4f);

    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer backgroundSprite;
    [SerializeField] private TMP_Text text;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.animator == null)
            this.animator = GetComponent<Animator>();

        if (this.backgroundSprite == null)
            this.backgroundSprite = GetComponentInChildren<SpriteRenderer>();

        if (this.text == null)
            this.text = GetComponentInChildren<TMP_Text>();
    }

    public void Setup(string text, float timeExist)
    {
        this.text.SetText(text);
        this.text.ForceMeshUpdate();

        Vector2 textSize = this.text.GetRenderedValues(false);
        Vector2 offset = new Vector2(-this.padding.x / 2f, 0f);
        this.backgroundSprite.size = textSize + this.padding;
        this.backgroundSprite.transform.localPosition = new Vector2(this.padding.x / 2f, 0f) + offset;

        this.animator.SetTrigger("FadeIn");

        Destroy(gameObject, timeExist);
    }
}
