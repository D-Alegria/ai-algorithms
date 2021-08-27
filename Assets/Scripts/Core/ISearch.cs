using System;

namespace Core
{
    public interface ISearch
    {
        public bool Search(Node currentNode, String targetValue);
    }
}