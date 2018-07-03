using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace VSRapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Dictionary<String, Node> listNodes = FactoryMethod<String, Node>.getList();
            foreach (var node in listNodes)
            {
                NodeList.Items.Add(node.Key);
            }
        }

        private void nodeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NodeList.SelectedItem != null)
            {
                AddNode.IsEnabled = true;
                int amount = Circuit.getAmount() + 1;
                NodeName.Text = "Node" + amount;
            }
            else
                AddNode.IsEnabled = false;
        }

        private void addNode(object sender, RoutedEventArgs e)
        {
            String nodeName = (String) NodeList.SelectedItem;
            String name = NodeName.Text;
            Node node = FactoryMethod<String, Node>.create(nodeName);
            node.setName(name);
            Circuit.addNode(name, node);

            updateConnectionList();
            
            NodeList.UnselectAll();
        }

        private void removeNode(object sender, RoutedEventArgs e)
        {
            String name = NodeName.Text;
            if (!Circuit.removeNode(name))
            {
                MessageBox.Show("No node with the name: " + name, "Unknown node name");
                return;
            }
            updateConnectionList();
        }

        private void updateConnectionList()
        {
            Dictionary<String, Node> circuitList = Circuit.getList();
            OutputNode.Items.Clear();
            InputNode.Items.Clear();
            if (circuitList.Count > 0)
            {
                foreach (var item in circuitList)
                {
                    OutputNode.Items.Add(item.Key);
                    InputNode.Items.Add(item.Key);
                }
                OutputNode.IsEnabled = true;
                InputNode.IsEnabled = true;
                AddConnection.IsEnabled = true;
                RemoveConnection.IsEnabled = true;
            }
            else
            {
                OutputNode.IsEnabled = false;
                InputNode.IsEnabled = false;
                AddConnection.IsEnabled = false;
                RemoveConnection.IsEnabled = false;
            }
        }

        private void addConnection(object sender, RoutedEventArgs e)
        {
            String outputNode = OutputNode.Text;
            String inputNode = InputNode.Text;
            Circuit.addConnection(outputNode, inputNode);
        }

        private void removeConnection(object sender, RoutedEventArgs e)
        {

        }
    }
}
