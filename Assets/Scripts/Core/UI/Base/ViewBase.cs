using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FairyGUI;

namespace UI {
    public abstract class ViewBase
    {
        public int ID;
        public bool IsDestroyed { get; set; }

        public bool IsActive { get; set; }

        public string Name => GetType().Name;

        public abstract void Initialize(ViewAttribute attribute);

        public virtual void Show()
        {
            OnShow();
            IsActive = true;
        }

        public virtual void OnShow() { }
        
        public virtual void Update() { }
        public virtual void OnHide() { }

        public virtual void Hide()
        {
            IsActive = false;
            OnHide();
        }

    }
}
