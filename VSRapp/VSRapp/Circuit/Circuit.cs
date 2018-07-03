using System;
using System.Collections.Generic;

namespace VSRapp
{
    public class Circuit
    {

        static private Circuit instance_ = null;
        private Dictionary<String, Node> nodes_ = new Dictionary<String, Node>();

        public static void addNode(String name, Node node)
        {
            Dictionary<String, Node> nodes = instance().nodes_;
            nodes.Add(name, node);
        }

        public static Boolean removeNode(String name)
        {
            Dictionary<String, Node> nodes = instance().nodes_;
            if (nodes.ContainsKey(name))
            {
                Node node = nodes[name];
                // TODO: remove connections
                nodes.Remove(name);
                return true;
            }
            return false;
        }

        public static int getAmount()
        {
            Dictionary<String, Node> nodes = instance().nodes_;
            return nodes.Count;
        }

        public static Dictionary<String, Node> getList()
        {
            return instance().nodes_;
        }

        private Circuit()
        {
            nodes_ = new Dictionary<String, Node>();
        }

        private static Circuit instance()
        {
            if (instance_ == null)
            {
                instance_ = new Circuit();
            }
            return instance_;
        }

    }
}