using FairyGUI;
using Main;

namespace UI
{
    [View("Main", "ThingDesView")]
    public class ThingDesView : FGUIView
    {
        public UI_ThingDesView _main;

        public override void OnShow()
        {
            base.OnShow();
            //TODO:ÿһ�δ򿪽���ִ��
        }

        public override void Bind(GComponent component)
        {
            base.Bind(component);
            _main = (UI_ThingDesView)component;
            //TODO:��һ�δ򿪽���ִ��
        }
    }

}
