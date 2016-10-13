using System.Collections.Generic;

namespace CozyThalassa.DesktopClient
{
    public class ImageSize
    {
        public int width = 128;
        public int height = 128;
    }

    public class NodeData
    {
        public string text;
        public string image;
        public ImageSize imageSize;

        public bool ShouldSerializeimage()
        {
            return (image != null);
        }
        public bool ShouldSerializeimageSize()
        {
            return (imageSize != null);
        }
    }

    public class Node
    {
        public NodeData data;
        public List<Node> children = new List<Node>();
    }

    public class Minder
    {
        public Node root;
    }
}
