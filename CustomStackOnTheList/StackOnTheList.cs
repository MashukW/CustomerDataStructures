using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomStackOnTheList
{
    public class StackOnTheList<T> :IEnumerable<T>
    {
        private StackNode<T> _head;
        private StackNode<T> _tail;

        public int Count { get; private set; }
                    
        public StackOnTheList()
        {
            Count = 0;
        }

        public StackOnTheList(IEnumerable<T> items)
        {
            if(items == null)
                throw new ArgumentNullException(nameof(items));

            foreach (var item in items)
                Push(item);
        }

        public void Push(T value)
        {
            if(value == null)
                throw new ArgumentNullException(nameof(value));

            StackNode<T> node = new StackNode<T>(value);

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

        public T Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException(nameof(Count));

            StackNode<T> result = _tail;

            if (Count == 1)
            {
                _head = null;
                _tail = null;
            }
            else
            {
                _tail = result.Previous;
                _tail.Next = null;
            }

            Count--;
            return result.Value;
        }

        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException(nameof(Count));

            return _tail.Value;
        }

        public bool Contains(T value)
        {
            if (Count == 0)
                return false;

            StackNode<T> current = _head;

            while (current != null)
            {
                if (current.Value.Equals(value))
                    return true;

                current = current.Next;
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            StackNode<T> current = _tail;

            while (current != null)
            {
                yield return current.Value;
                current = current.Previous;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
