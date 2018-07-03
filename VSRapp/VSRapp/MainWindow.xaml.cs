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
            Circuit.addNode(name, node);

            Dictionary<String, Node> circuitList = Circuit.getList();
            Node1.Items.Clear();
            Node2.Items.Clear();
            foreach (var item in circuitList)
            {
                Node1.Items.Add(item.Key);
                Node2.Items.Add(item.Key);
            }
            Node1.IsEnabled = true;
            Node2.IsEnabled = true;
            AddConnection.IsEnabled = true;
            RemoveConnection.IsEnabled = true;

            NodeList.UnselectAll();
        }

        private void removeNode(object sender, RoutedEventArgs e)
        {

        }
    }
}
