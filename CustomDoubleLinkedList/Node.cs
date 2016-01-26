using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDoubleLinkedList
{
    public class Node<T>
    {
        public Node(T value)
        {
            Value = value;
        }
        
        public T Value { get; }

        public Node<T> Previous { get; set; }

        public Node<T> Next { get; set; }
    }
}
