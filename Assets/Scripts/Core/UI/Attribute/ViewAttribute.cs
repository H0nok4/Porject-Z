using System;


namespace UI {
    public class ViewAttribute : Attribute
    {
        public string PackageName;

        public string ComponentName;

        public ViewAttribute(string packageName, string componentName) {
            PackageName = packageName;
            ComponentName = componentName;
        }
    }
}
