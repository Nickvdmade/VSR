using System;
using System.Collections.Generic;

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
        public abstract void save(String fileName, Dictionary<String, Node> circuit);
        public abstract void open(String fileName);
        public abstract String getKey();
        public abstract object Clone();
    }
}