using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FairyGUI;
using UnityEngine;

namespace UI {
    public struct CmdData
    {
        public string CmdName;

        public List<object> Param;

        public bool TryGetParam<T>(int index, out T result)
        {
            if (Param[index] is T)
            {
                result = (T)Param[index];
                return true;
            }

            result = default(T);
            return false;
        }
    }
    public class FGUIView : ViewBase
    {

        public class FGUIWindow : Window
        {
            public FGUIView ViewView { get; }

            public FGUIWindow(FGUIView viewView) {
                ViewView = viewView;
                fairyBatching = true;
            }

            protected override void OnInit()
            {
                base.OnInit();
                if (contentPane == null)
                {
                    contentPane = (GComponent)UIPackage.CreateObject(ViewView.PackageName, ViewView.ComponentName);

                    if (contentPane == null)
                    {
                        Debug.LogError($"ContentPanel创建失败,Name = {ViewView.PackageAndComponentName}");
                        return;
                    }

                    AdaptToScreen();

                    ViewView.Bind(contentPane);
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

        public virtual void OnCmd(CmdData data)
        {
            
        }
    }
}
