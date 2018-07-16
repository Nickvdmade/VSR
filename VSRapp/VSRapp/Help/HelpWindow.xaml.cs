using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace VSRapp.Help
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();

            HelpText.TextWrapping = TextWrapping.Wrap;
            HelpText.Inlines.Add("This program was designed to create files that represent logical circuits.\n\n");
            HelpText.Inlines.Add(new Bold(new Run("Toolbar buttons\n\n")));
            HelpText.Inlines.Add(new Italic(new Underline(new Run("New\n"))));
            HelpText.Inlines.Add(
                "This will create a new circuit in the canvas after asking if you want to save the current circuit.\n\n");
            HelpText.Inlines.Add(new Italic(new Underline(new Run("Open\n"))));
            HelpText.Inlines.Add("This will open a separate window where you can search for a file to open.\n" +
                                 "After a file is selected, the program will create the circuit in the canvas according to the selected file.\n" +
                                 "Make sure that the selected file can be processed by the program.\n\n");
            HelpText.Inlines.Add(new Italic(new Underline(new Run("Save\n"))));
            HelpText.Inlines.Add(
                "This will open a separate window where you can set the name of the file to save to.\n" +
                "After naming the file, the program will create the file according to the current circuit.\n\n");
            HelpText.Inlines.Add(new Italic(new Underline(new Run("Help\n"))));
            HelpText.Inlines.Add("This will open the help screen in a separate window.\n\n");

            HelpText.Inlines.Add(new Bold(new Run("Node operations\n\n")));
            HelpText.Inlines.Add(new Italic(new Underline(new Run("Add new node\n"))));
            HelpText.Inlines.Add("In order to add a new node to the circuit, select a gate from the list of possible gates." +
                                 "After selecting a type of gate, give a name to the node. There will be a standard name given if no custom name was given." +
                                 "After a name has been given to the node, press the ");
            HelpText.Inlines.Add(new Italic(new Run("Add node")));
            HelpText.Inlines.Add(" button to add the node to the circuit.\n\n");
            HelpText.Inlines.Add(new Italic(new Underline(new Run("Remove existing node\n"))));
            HelpText.Inlines.Add(
                "In order to remove an existing nodes from the circuit, type the name of the node in the field." +
                "After that, press the ");
            HelpText.Inlines.Add(new Italic(new Run("Remove node")));
            HelpText.Inlines.Add(" button to remove the node from the circuit." +
                                 "This will also remove the connections from and to the node.\n\n");
            
            HelpText.Inlines.Add(new Bold(new Run("Connection operations\n\n")));
            HelpText.Inlines.Add(new Italic(new Underline(new Run("Adding connections\n"))));
            HelpText.Inlines.Add(
                "In order to add a connection to the circuit, select an output and an input node from the list of nodes." +
                "After the selection has been made, press the ");
            HelpText.Inlines.Add(new Italic(new Run("Add connection")));
            HelpText.Inlines.Add(" button to add the connection between the two nodes.\n\n");
            HelpText.Inlines.Add(new Italic(new Underline(new Run("Removing connections\n"))));
            HelpText.Inlines.Add(
                "In order to remove a connection from the circuit, select an output and an input node from the list of nodes." +
                "After the selection has been made, press the ");
            HelpText.Inlines.Add(new Italic(new Run("Remove connection")));
            HelpText.Inlines.Add(" button to remove the connection between the two nodes.\n\n");

            HelpText.Inlines.Add(new Bold(new Run("Canvas operations")));
        }

        public void closeWindow(object sender, CancelEventArgs e)
        {
            MainWindow.closeHelp();
        }
    }
}
