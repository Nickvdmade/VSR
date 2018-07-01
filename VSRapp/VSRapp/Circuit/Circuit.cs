﻿using System;
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