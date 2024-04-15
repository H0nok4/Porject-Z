using UnityEngine;

public class ThingObject {
    public GameObject GO;

    public SpriteRenderer SpriteRenderer;
    public GameObject Selector;
    public ThingObject(GameObject go) {
        GO = go;
        SpriteRenderer = go.transform.GetChild(1).GetComponentInChildren<SpriteRenderer>();
        Selector = go.transform.GetChild(0).gameObject;
    }

    public void SetPosition(PosNode pos) {
        GO.transform.localPosition = pos.Pos.ToVector3();
    }

    public void Select()
    {
        Selector.SetActive(true);
    }

    public void DeSelect()
    {
        Selector.SetActive(false);
    }

    public void SetSprite(Sprite sprite) {
        SpriteRenderer.sprite = sprite;
    }

    public void Dispose()
    {
        //TODO:后面可以用对象池管理
        SpriteRenderer = null;
        GameObject.Destroy(this.GO);
    }
}