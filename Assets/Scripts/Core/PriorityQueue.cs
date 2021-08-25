using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class PriorityQueue<TEntry> where TEntry : IPrioritizable
    {
        public LinkedList<TEntry> Entries { get; } = new LinkedList<TEntry>();

        public int Count()
        {
            return Entries.Count;
        }

        public TEntry Dequeue()
        {
            if (Entries.Any())
            {
                var itemTobeRemoved = Entries.First.Value;
                Entries.RemoveFirst();
                return itemTobeRemoved;
            }

            return default(TEntry);
        }

        public void Enqueue(TEntry entry)
        {
            var value = new LinkedListNode<TEntry>(entry);
            if (Entries.First == null)
            {
                Entries.AddFirst(value);
            }
            else
            {
                var ptr = Entries.First;
                while (ptr.Next != null && ptr.Value.Cost < entry.Cost)
                {
                    ptr = ptr.Next;
                }

                if (ptr.Value.Cost <= entry.Cost)
                {
                    Entries.AddAfter(ptr, value);
                }
                else
                {
                    Entries.AddBefore(ptr, value);
                }
            }
        }
    }
}

public interface IPrioritizable
{
    /// <summary>
    /// Priority of the item.
    /// </summary>
    Node Node { get; set; }

    int Cost { get; set; }
}

class PriorityNode : IPrioritizable
{
    public Node Node { get; set; }
    public int Cost { get; set; }

    public PriorityNode(Node node, int cost)
    {
        Node = node;
        Cost = cost;
    }
}