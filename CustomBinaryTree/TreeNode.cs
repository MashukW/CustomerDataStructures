using System;

namespace CustomBinaryTree
{
    public class TreeNode<T> : IComparable<T> where T : IComparable<T>
    {
        public TreeNode(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public T Value { get; }

        public TreeNode<T> Parent { get; set; }
        public TreeNode<T> Right { get; set; }
        public TreeNode<T> Left { get; set; }
        
        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
        }
    }
}
