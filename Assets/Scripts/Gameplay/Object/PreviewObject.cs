using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    private SpriteRenderer _preivewSpriteRenderer;

    public bool Valid
    {
        set
        {
            if (!value)
            {
                _preivewSpriteRenderer.color = Color.red;
                return;
            }

            _preivewSpriteRenderer.color = Color.white;
        }
    }
    private void Start()
    {
        _preivewSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    public void SetSprite(Sprite sprite)
    {
        _preivewSpriteRenderer.sprite = sprite;
    }

}