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
            Dictionary<String,Node> listNodes = FactoryMethod<String,Node>.getList();
            foreach (var node in listNodes)
            {
                NodeList.Items.Add(node.Key);
            }
        }

        private void nodeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NodeList.SelectedItem != null)
                AddNode.IsEnabled = true;
            else
                AddNode.IsEnabled = false;
        }

        private void addNode(object sender, RoutedEventArgs e)
        {
            String nodeName = (String) NodeList.SelectedItem;
            // TODO: receive name from user
            String name = "test";
            Node node = FactoryMethod<String, Node>.create(nodeName);
            Circuit.addNode(name, node);

            NodeList.UnselectAll();
        }
    }
}
