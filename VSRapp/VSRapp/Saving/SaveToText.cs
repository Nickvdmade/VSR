namespace VSRapp
{
    public class SaveToText : Save
    {
        public SaveToText()
        {
        }

        public override string getKey()
        {
            return "SaveToText";
        }

        public override object Clone()
        {
            return new SaveToText();
        }
    }
}