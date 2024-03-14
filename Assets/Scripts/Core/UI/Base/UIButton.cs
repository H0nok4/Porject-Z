using System;
using UI;
using UnityEngine.EventSystems;

namespace UI
{
    public class UIButton : UIComponent {
        public Action OnClick;

        public override void OnPointerClick(PointerEventData eventData) {
            OnClick?.Invoke();
        }

        public override void InitInstance() {

        }
    }
}
