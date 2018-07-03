using System;
using System.Collections.Generic;
using System.Reflection;

namespace VSRapp
{
    public class FactoryMethod<TKey, TObject>
        where TObject : ICloneable, IGetKey<TKey>
    {

        private static Dictionary<TKey, TObject> objectMap_;
        static private FactoryMethod<TKey, TObject> instance_ = null;

        public static TObject create(TKey key)
        {
            return instance()._create(key);
        }

        public static Dictionary<TKey, TObject> getList()
        {
            instance();
            return objectMap_;
        }

        private FactoryMethod()
        {
            objectMap_ = new Dictionary<TKey, TObject>();
        }

        private static FactoryMethod<TKey, TObject> instance()
        {
            if (instance_ == null)
            {
                instance_ = new FactoryMethod<TKey, TObject>();
                instance_.initialize();
            }
            return instance_;
        }

        private TObject _create(TKey key)
        {
            if (objectMap_.ContainsKey(key))
            {
                TObject cloneObject = objectMap_[key];
                return (TObject)cloneObject.Clone();
            }
            return default(TObject);
        }

        private void initialize()
        {
            Type[] types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            Type baseType = typeof(TObject);
            TObject currentObject;

            foreach (Type type in types)
            {
                if (ableToMake(type) && inheritedFromTObject(type, baseType))
                {
                    currentObject = getDefaultInstance(type);
                    if (currentObject != null)
                        objectMap_.Add(currentObject.getKey(), currentObject);
                }
            }
        }

        private bool ableToMake(Type type)
        {
            return !type.IsPrimitive && !type.IsAbstract;
        }

        private bool inheritedFromTObject(Type type, Type typeTObject)
        {
            Type baseType, next;

            next = type.BaseType;
            baseType = next;
            while (next != null && baseType.FullName != typeTObject.FullName)
            {
                baseType = next;
                next = baseType.BaseType;
            }

            return next != null && baseType.FullName == typeTObject.FullName;
        }

        private TObject getDefaultInstance(Type type)
        {
            ConstructorInfo[] constructorInfo = type.GetConstructors();

            for (int n = 0; n < constructorInfo.Length; n++)
            {
                if (constructorInfo[n].GetParameters().Length == 0)
                    return (TObject) constructorInfo[n].Invoke(null);
            }

            return default(TObject);
        }

    }

    public interface IGetKey<TKey>
    {
        TKey getKey();
    }
}