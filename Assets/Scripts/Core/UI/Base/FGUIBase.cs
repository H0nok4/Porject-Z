using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FairyGUI;
using UnityEngine;

namespace UI {
    public class FGUIBase : UIBase
    {

        public class FGUIWindow : Window
        {
            public FGUIBase BaseView { get; }

            public FGUIWindow(FGUIBase baseView) {
                BaseView = baseView;
                fairyBatching = true;
            }

            protected override void OnInit()
            {
                base.OnInit();
                if (contentPane == null)
                {
                    contentPane = (GComponent)UIPackage.CreateObject(BaseView.PackageName, BaseView.ComponentName);

                    if (contentPane == null)
                    {
                        Debug.LogError($"ContentPanel创建失败,Name = {BaseView.PackageAndComponentName}");
                        return;
                    }

                    AdaptToScreen();

                    BaseView.Bind(contentPane);
                }
            }

            public void AdaptToScreen()
            {
                SetSize(Screen.width,Screen.height);
                SetXY(0,0);
            }
        }
        public FGUIWindow Win { get; private set; }

        public string PackageName { get; private set; }

        public string ComponentName { get; private set; }

        public string PackageAndComponentName => $"{PackageName}/{ComponentName}";
        public override void Initialize(ViewAttribute attribute)
        {
            PackageName = attribute.PackageName;
            ComponentName = attribute.ComponentName;
            Win = new FGUIWindow(this);
            //TODO:
            UIPackageManager.AddPackage(PackageName);
            ShowWin();
        }

        public sealed override void Show()
        {
            
            if (IsActive)
            {
                return;
            }

            ShowWin();

            base.Show();
        }

        public void ShowWin()
        {
            if (Win != null && !Win.isShowing)
            {
                Win.Show();
            }
        }

        public void HideWin()
        {
            if (Win != null && Win.isShowing)
            {
                Win.Hide();
            }
        }

        public sealed override void Hide()
        {
            if (!IsActive)
            {
                return;
            }

            HideWin();

            base.Hide();
        }
        public virtual void Bind(GComponent component) {

        }
    }
}
