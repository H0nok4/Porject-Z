using System.Collections;
using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;

public class BtnBuildWall : UIButton
{
    public TMP_Text Text;

    public override void InitInstance()
    {
        Text = GetTextAtChildIndex(0);
    }
}
