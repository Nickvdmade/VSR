using System;
using System.Collections.Generic;
using System.Windows;

namespace VSRapp
{
    public abstract class FileType : ICloneable, IGetKey<String>
    {
        protected FileType()
        {
        }

        public static FileType create(String name)
        {
            return FactoryMethod<String, FileType>.create(name);
        }

        public abstract String getFilter();

        public virtual void save(String fileName, Dictionary<String, Node> circuit)
        {
            MessageBox.Show("Can't save to this file type", "Save file");
        }

        public virtual void open(String fileName)
        {
            MessageBox.Show("Can't open this file type", "Open file");
        }

        public abstract String getKey();
        public abstract object Clone();
    }
}