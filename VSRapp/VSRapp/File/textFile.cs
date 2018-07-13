using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VSRapp
{
    public class TextFile : FileType
    {
        public TextFile()
        {
        }

        public override String getFilter()
        {
            return "txt files (*.txt)|*.txt|";
        }

        public override void save(string fileName, Dictionary<String, Node> circuit)
        {
            Queue<Node> queue = new Queue<Node>();
            String types = "";
            String connections = "";

            foreach (var node in circuit)
                if (node.Value.getKey() == "INPUT_HIGH" || node.Value.getKey() == "INPUT_LOW")
                    queue.Enqueue(node.Value);

            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                types += node.getName() + ":\t" + node.getKey() + ";\r\n";

                if (node.getKey() != "PROBE")
                {
                    List<Node> outputs = node.getOutputs();
                    String connection = node.getName() + ":\t";
                    foreach (var output in outputs)
                    {
                        connection += output.getName() + ", ";
                        Node nextNode = output.useForSave();
                        if (nextNode != null)
                            queue.Enqueue(nextNode);
                    }
                    int index = connection.LastIndexOf(',');
                    connections += connection.Substring(0, index) + ";\r\n";
                }
            }

            String file = types + "\r\n" + connections;
            File.WriteAllText(fileName, file);
        }

        public override void open(string fileName)
        {
            String[] allLines = File.ReadAllLines(fileName);
            int part = 0;
            List<String> lines = new List<String>();

            foreach (var line in allLines)
            {
                if (Char.IsLetter(line.FirstOrDefault()))
                {
                    if (part == 0)
                        part = 1;
                    if (part == 1)
                        lines.Add(line);
                    if (part == 2)
                        part = 3;
                    if (part == 3)
                        lines.Add(line);
                }
                else
                {
                    if (part == 1)
                    {
                        createNodes(lines);
                        lines.Clear();
                        part = 2;
                    }
                }
            }
            createConnections(lines);
        }

        private void createNodes(List<String> lines)
        {
            foreach (String line in lines)
            {
                int startPosition = 0;
                int position = line.IndexOf(':');
                String name = line.Substring(startPosition, position);
                int lastSpace = line.LastIndexOf(' ');
                int lastTab = line.LastIndexOf('\t');
                startPosition = Math.Max(lastSpace, lastTab) + 1;
                position = line.IndexOf(';', startPosition);
                String type = line.Substring(startPosition, position - startPosition);
                Node node = FactoryMethod<String, Node>.create(type);
                node.setName(name);
                Circuit.addNode(name, node);
            }
        }

        private void createConnections(List<String> lines)
        {
            foreach (var line in lines)
            {
                int startPosition = 0;
                int position = line.IndexOf(':');
                String name = line.Substring(startPosition, position);
                int lastSpace = line.LastIndexOf(' ');
                int lastTab = line.LastIndexOf('\t');
                startPosition = Math.Max(lastSpace, lastTab) + 1;
                position = line.IndexOf(',', startPosition);
                String connection;
                while (position != -1)
                {
                    connection = line.Substring(startPosition, position - startPosition);
                    Circuit.addConnection(name, connection);
                    startPosition = position + 1;
                    position = line.IndexOf(',', startPosition);
                }
                position = line.IndexOf(';', startPosition);
                connection = line.Substring(startPosition, position - startPosition);
                Circuit.addConnection(name, connection);
            }
        }

        public override String getKey()
        {
            return "txt";
        }

        public override object Clone()
        {
            return new TextFile();
        }
    }
}