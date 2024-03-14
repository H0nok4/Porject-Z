using UnityEngine;

public class ThingObject {
    public GameObject GO;

    public SpriteRenderer SpriteRenderer;
    public ThingObject(GameObject go) {
        GO = go;
        SpriteRenderer = go.GetComponentInChildren<SpriteRenderer>();
    }

    public void SetPosition(IntVec2 pos) {
        GO.transform.localPosition = pos.ToVector3();
    }

    public void SetSprite(Sprite sprite) {
        SpriteRenderer.sprite = sprite;
    }
}