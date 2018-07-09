using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace VSRapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Point startingPoint_;
        private static Point endPoint_;
        private static Boolean itemSelected_;
        private static Boolean horizontal_;
        private static Boolean vertical_;
        private static Image image_;
        private static Canvas circuitCanvas_;

        public MainWindow()
        {
            InitializeComponent();

            circuitCanvas_ = CircuitCanvas;

            Dictionary<String, Node> listNodes = FactoryMethod<String, Node>.getList();
            foreach (var node in listNodes)
            {
                NodeList.Items.Add(node.Key);
            }
        }

        public static void addImage(Image imageAdd, TextBlock textAdd)
        {
            imageAdd.MouseLeftButtonDown += selectItemHorizontal;
            imageAdd.MouseLeftButtonUp += deselectItemHorizontal;
            imageAdd.MouseRightButtonDown += selectItemVertical;
            imageAdd.MouseRightButtonUp += deselectItemVertical;
            imageAdd.MouseMove += moveItem;
            circuitCanvas_.Children.Add(imageAdd);
            circuitCanvas_.Children.Add(textAdd);
            Point location = imageAdd.TranslatePoint(new Point(0, 0), circuitCanvas_);
            Canvas.SetLeft(textAdd, location.X);
            Canvas.SetTop(textAdd, location.Y + imageAdd.Height);
        }

        public static void removeElement(UIElement toRemove)
        {
            circuitCanvas_.Children.Remove(toRemove);
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
            Circuit.removeNode(name);
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
            String outputNode = OutputNode.Text;
            String inputNode = InputNode.Text;
            Circuit.removeConnection(outputNode, inputNode);
        }

        private static void selectItemHorizontal(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startingPoint_ = Mouse.GetPosition(circuitCanvas_);
            itemSelected_ = true;
            horizontal_ = true;
            image_ = sender as Image;
        }

        private static void deselectItemHorizontal(object sender, MouseButtonEventArgs e)
        {
            endPoint_ = Mouse.GetPosition(circuitCanvas_);
            if (horizontal_)
                if (Math.Abs(endPoint_.X - startingPoint_.X) >= 10)
                {
                    double x = endPoint_.X - startingPoint_.X;
                    Point location = image_.TranslatePoint(new Point(0, 0), circuitCanvas_);
                    Canvas.SetLeft(image_, location.X + x);
                    startingPoint_ = endPoint_;
                }
            updateText();
            
            itemSelected_ = false;
            horizontal_ = false;
            image_ = null;
        }

        private static void selectItemVertical(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            startingPoint_ = Mouse.GetPosition(circuitCanvas_);
            itemSelected_ = true;
            vertical_ = true;
            image_ = sender as Image;
        }

        private static void deselectItemVertical(object sender, MouseButtonEventArgs e)
        {
            endPoint_ = Mouse.GetPosition(circuitCanvas_);
            if (vertical_)
                if (Math.Abs(endPoint_.Y - startingPoint_.Y) >= 10)
                {
                    double y = endPoint_.Y - startingPoint_.Y;
                    Point location = image_.TranslatePoint(new Point(0, 0), circuitCanvas_);
                    Canvas.SetTop(image_, location.Y + y);
                    startingPoint_ = endPoint_;
                }
            updateText();

            itemSelected_ = false;
            vertical_ = false;
            image_ = null;
        }

        private static void moveItem(object sender, MouseEventArgs e)
        {
            if (itemSelected_)
            {
                endPoint_ = Mouse.GetPosition(circuitCanvas_);
                if (horizontal_)
                    if (Math.Abs(endPoint_.X - startingPoint_.X) >= 10)
                    {
                        double x = endPoint_.X - startingPoint_.X;
                        Point location = image_.TranslatePoint(new Point(0, 0), circuitCanvas_);
                        Canvas.SetLeft(image_, location.X + x);
                        startingPoint_ = endPoint_;
                    }
                if (vertical_)
                    if (Math.Abs(endPoint_.Y - startingPoint_.Y) >= 10)
                    {
                        double y = endPoint_.Y - startingPoint_.Y;
                        Point location = image_.TranslatePoint(new Point(0, 0), circuitCanvas_);
                        Canvas.SetTop(image_, location.Y + y);
                        startingPoint_ = endPoint_;
                    }
                updateText();
            }
        }

        private static void updateText()
        {
            TextBlock text = Circuit.getRelativeText(image_);
            Point location = image_.TranslatePoint(new Point(0, 0), circuitCanvas_);
            Canvas.SetLeft(text, location.X);
            Canvas.SetTop(text, location.Y + image_.Height);
        }
    }
}
