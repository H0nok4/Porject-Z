using UnityEngine;

public interface IThingObject {
    public GameObject GO { get; set; }

    public SpriteRenderer SpriteRenderer { get; set; }

    public GameObject Selector { get; set; }

    void SetPosition(PosNode node);

    void Select();

    void DeSelect();

    void SetSprite(Sprite sprite);

    void Dispose();
}

public class ThingObject : IThingObject {
    public GameObject GO { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public GameObject Selector { get; set; }

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