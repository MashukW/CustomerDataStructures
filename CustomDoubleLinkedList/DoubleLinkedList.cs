using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomDoubleLinkedList
{
    public class DoubleLinkedList<T> : IEnumerable<T>
    {
        private Node<T> _head;
        private Node<T> _tail;

        #region Сonstructors

        public DoubleLinkedList()
        {
            Count = 0;
        }

        public DoubleLinkedList(IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
                AddLast(item);
        }

        #endregion

        #region Properties
        
        public Node<T> First
        {
            get
            {
                if(Count == 0)
                    throw new InvalidOperationException(nameof(_head));

                return _head;
            }
        }

        public Node<T> Last
        {
            get
            {
                if (Count == 0)
                    throw new InvalidOperationException(nameof(_tail));

                return _tail;
            }
        }

        public int Count { get; private set; }

        #endregion
        
        #region Methods

        public void AddFirst(T value)
        {
            if (value == null)
                throw new InvalidOperationException(nameof(value));

            Node<T> node = new Node<T>(value);

            if (Count == 0)
            {
                _tail = node;
            }
            else
            {
                _head.Previous = node;
                node.Next = _head;
            }
            _head = node;

            Count++;
        }

        public void AddLast(T value)
        {
            if (value == null)
                throw new InvalidOperationException(nameof(value));

            Node<T> node = new Node<T>(value);

            if (Count == 0)
            {
                _head = node;
            }
            else
            {
                _tail.Next = node;
                node.Previous = _tail;
            }

            _tail = node;

            Count++;
        }

        public bool Contains(T item)
        {
            if (item == null)
                return false;

            Node<T> current = _head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                    return true;

                current = current.Next;
            }

            return false;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public bool Remove(T item)
        {
            Node<T> current = _head;

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    if (current.Previous != null)
                    {
                        current.Previous.Next = current.Next;

                        if (current.Next == null)
                            _tail = current.Previous;
                        else
                            current.Next.Previous = current.Previous;

                        Count--;
                    }
                    else
                    { 
                        RemoveFirst();
                    }

                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public void RemoveFirst()
        {
            if (Count == 0)
                throw new InvalidOperationException(nameof(Count));

            _head = _head.Next;
            Count--;

            if (Count == 0)
                _tail = null;
            else
                _head.Previous = null;
        }

        public void RemoveLast()
        {
            if(Count == 0)
                throw new InvalidOperationException(nameof(Count));
            
            if (Count == 1)
            {
                _head = null;
                _tail = null;
            }
            else
            {
                _tail.Previous.Next = null;
                _tail = _tail.Previous;
            }

            Count--;
        }

        public Node<T> AddAfter(Node<T> node, T value)
        {
            if(node == null)
                throw new InvalidOperationException(nameof(node));

            Node<T> current = _head;

            while (current.Next != null)
            {
                if (current.Value.Equals(node.Value))
                {
                    Node<T> newNode = new Node<T>(value);

                    if (current.Next == null)
                    {
                        _tail = newNode;
                    }
                    else
                    {
                        newNode.Next = current.Next;
                        newNode.Next.Previous = newNode;
                    }

                    newNode.Previous = current;
                    current.Next = newNode;

                    Count++;
                    return newNode;
                }

                current = current.Next;
            }

            return null;
        }

        public Node<T> AddBefore(Node<T> node, T value)
        {
            if (node == null)
                throw new InvalidOperationException(nameof(node));

            Node<T> current = _head;

            while (current.Next != null)
            {
                if (current.Value.Equals(node.Value))
                {
                    Node<T> newNode = new Node<T>(value);

                    if (current.Previous == null)
                    {
                        current.Previous = newNode;
                        newNode.Next = current;
                        _head = newNode;
                    }
                    else
                    {
                        newNode.Next = current;
                        newNode.Previous = current.Previous;
                        newNode.Next.Previous = newNode;
                        newNode.Previous.Next = newNode;
                    }

                    Count++;
                    return newNode;
                }

                current = current.Next;
            }

            return null;
        }
        
        public void CopyTo(T[] array, int index)
        {
            if (array == null)
                throw new NullReferenceException(nameof(array));

            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if(Count > array.Length - index)
                throw new ArgumentException(nameof(array));

            Node<T> current = _head;

            for (int i = index; i <= Count; i++)
            {
                array[i] = current.Value;
                current = current.Next;
            }
        }
        
        public Node<T> Find(T value)
        {

            if (value == null)
                return null;

            Node<T> current = _head;

            while (current != null)
            {
                if (current.Value.Equals(value))
                    return current;

                current = current.Next;
            }

            return null;
        }
        
        #endregion

        #region Interface IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return new CustomIterator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private struct CustomIterator : IEnumerator<T>
        {
            private readonly DoubleLinkedList<T> _doubleLinkedList;
            private Node<T> _current;

            public CustomIterator(DoubleLinkedList<T> collection)
            {
                if (collection == null)
                    throw new ArgumentNullException(nameof(collection));

                _doubleLinkedList = collection;
                _current = null;
            }

            public T Current
            {
                get
                {
                    if (_current == null)
                        throw new NullReferenceException(nameof(_current));

                    return _current.Value;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }
            
            public bool MoveNext()
            {
                if (_current == null)
                {
                    _current = _doubleLinkedList.First;
                    return true;
                }
                if (_current.Next != null)
                {
                    _current = _current.Next;
                    return true;
                }

                Reset();
                return false;
            }

            public void Reset()
            {
                _current = null;
            }

            public void Dispose()
            {

            }
        }

        #endregion
    }
}
