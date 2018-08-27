using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldObject : MonoBehaviour {
    public enum ObjectType { TestTile, ThunderCloud, RainCloud,Tree }

    public ObjectType objectType;

    public SpriteRenderer spriteRenderer;

    public delegate void Foo();

    public event Foo Click;

    public void initialization() {
        Click += Clickdoing;

    }

    public void OnDisable()
    {
        Click -= Clickdoing;

    }

    public Sprite GetSprite() {
        return spriteRenderer.sprite;
    }

    public void SetSprite(Sprite sprite) {
        spriteRenderer.sprite = sprite;
    }

    public virtual void Clickdoing() {

    }

    public void OnClick() {
        if (Click != null) {
            Click();
        }
    }

}
