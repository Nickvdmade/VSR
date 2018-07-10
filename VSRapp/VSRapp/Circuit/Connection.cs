namespace VSRapp
{
    public class Connection
    {
        private Node fromNode_;
        private Node toNode_;

        public Connection(Node fromNode, Node toNode)
        {
            fromNode_ = fromNode;
            toNode_ = toNode;
        }

        public Node getFrom()
        {
            return fromNode_;
        }

        public Node getTo()
        {
            return toNode_;
        }
    }
}
