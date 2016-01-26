using System;

namespace CustomStackOnTheList
{
    public class StackNode<T>
    {
        public StackNode(T value)
        {
            if (value == null)
                throw new ArgumentNullException();

            Value = value;
        }

        public T Value { get; }

        public StackNode<T> Next { get; set; }
        public StackNode<T> Previous { get; set; } 
    }
}
