using System;

namespace VSRapp
{
    public abstract class Node : ICloneable, IGetKey<String>
    {
        protected Node()
        {
        }

        public static Node create(String name)
        {
            return FactoryMethod<String, Node>.create(name);
        }

        public abstract String getKey();
        public abstract object Clone();
    }
}