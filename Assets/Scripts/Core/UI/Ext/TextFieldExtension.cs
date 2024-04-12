

namespace FairyGUI {
    public static class TextFieldExtension {
        public static void Set(this GTextField text,string value)
        {
            text.text = value;
        }

        public static void RefreshValue(this GTextField text, string template, object value)
        {
            text.SetVar(template, value.ToString());
            text.FlushVars();
        }

        
    }
}
