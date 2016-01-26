using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomBinaryTree
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private TreeNode<T> _head;

        public int Count { get; private set; }

        #region Constructors

        public BinaryTree()
        {
            Count = 0;
        }

        public BinaryTree(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
                Add(item);
        }

        #endregion

        #region Methods

        public void Add(T value)
        {
            if (value == null)
                throw new InvalidOperationException(nameof(value));

            if (_head == null)
                _head = new TreeNode<T>(value);
            else
                AddTo(_head, value);

            Count++;
        }

        public bool Remove(T value)
        {
            TreeNode<T> removeNode = Search(value);

            if (removeNode == null)
                return false;

            Count--;

            if (removeNode.Right == null)
                Transplant(removeNode, removeNode.Left);

            else if (removeNode.Left == null)
                Transplant(removeNode, removeNode.Right);

            else
            {
                TreeNode<T> leftmost = removeNode.Right;

                while (leftmost.Left != null)
                    leftmost = leftmost.Left;

                if (leftmost.Parent != removeNode)
                {
                    Transplant(leftmost, leftmost.Right);
                    leftmost.Right = removeNode.Right;
                    leftmost.Right.Parent = leftmost;
                }

                Transplant(removeNode, leftmost);
                leftmost.Left = removeNode.Left;
                leftmost.Left.Parent = leftmost;
            }

            return true;
        }

        public void Clear()
        {
            _head = null;
            Count = 0;
        }

        public bool Contain(T value)
        {
            return Search(value) != null;
        }

        public TreeNode<T> Search(T value)
        {
            TreeNode<T> current = _head;

            while (current != null)
            {
                int result = current.CompareTo(value);

                if (result > 0)
                    current = current.Left;
                else if (result < 0)
                    current = current.Right;
                else
                    return current;
            }

            return null;
        }

        public T Minimum()
        {
            if (Count == 0)
                throw new InvalidOperationException(nameof(Count));

            TreeNode<T> current = _head;

            while (current.Left != null)
                current = current.Left;

            return current.Value;
        }

        public T Maximum()
        {
            if (Count == 0)
                throw new InvalidOperationException(nameof(Count));

            TreeNode<T> current = _head;

            while (current.Right != null)
                current = current.Right;

            return current.Value;
        }

        #region Прямой порядок (PreOrderTraversal)

        public IEnumerable<T> PreOrderTraversal()
        {
            List<T> values = new List<T>();
            PreOrderTraversal(_head, ref values);
            return values;
        }
        private void PreOrderTraversal(TreeNode<T> node, ref List<T> values)
        {
            if (node == null)
                return;

            values.Add(node.Value);
            PreOrderTraversal(node.Left, ref values);
            PreOrderTraversal(node.Right, ref values);
        }

        #endregion

        #region Обратный порядок (PostOrderTraversal)

        public IEnumerable<T> PostOrderTraversal()
        {
            List<T> values = new List<T>();
            PostOrderTraversal(_head, ref values);
            return values;
        }
        private void PostOrderTraversal(TreeNode<T> node, ref List<T> values)
        {
            if (node == null)
                return;

            PostOrderTraversal(node.Left, ref values);
            PostOrderTraversal(node.Right, ref values);
            values.Add(node.Value);
        }

        #endregion

        #region Семметричный порядок (InOrderTraversal)

        public IEnumerable<T> InOrderTraversal()
        {
            List<T> values = new List<T>();
            InOrderTraversal(_head, ref values);
            return values;
        }
        private void InOrderTraversal(TreeNode<T> node, ref List<T> values)
        {
            if (node == null)
                return;

            InOrderTraversal(node.Left, ref values);
            values.Add(node.Value);
            InOrderTraversal(node.Right, ref values);
        }

        #endregion

        #endregion

        #region IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Private

        private void AddTo(TreeNode<T> node, T value)
        {
            if (value.CompareTo(node.Value) < 0)
            {
                if (node.Left == null)
                    node.Left = new TreeNode<T>(value) { Parent = node };
                else
                    AddTo(node.Left, value);
            }
            else
            {
                if (node.Right == null)
                    node.Right = new TreeNode<T>(value) { Parent = node };
                else
                    AddTo(node.Right, value);
            }
        }

        private void Transplant(TreeNode<T> currentNode, TreeNode<T> replaceNode)
        {
            if (currentNode.Parent == null)
                _head = replaceNode;
            else if (currentNode == currentNode.Parent.Left)
                currentNode.Parent.Left = replaceNode;
            else
                currentNode.Parent.Right = replaceNode;

            if (replaceNode != null)
                replaceNode.Parent = currentNode.Parent;
        }

        #endregion
    }
}
