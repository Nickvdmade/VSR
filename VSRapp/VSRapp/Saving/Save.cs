using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VSRapp
{
    public abstract class Save : ICloneable, IGetKey<String>
    {
        protected Save()
        {
        }

        public static Save create(String name)
        {
            return FactoryMethod<String, Save>.create(name);
        }

        public abstract String getKey();
        public abstract object Clone();
    }
}