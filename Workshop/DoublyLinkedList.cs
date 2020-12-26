using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Workshop
{
    public class DoublyLinkedList<T> : IEnumerable
    {
        public class Node
        {
            public Node(T value)
            {
                Value = value;
            }

            public Node NextNode { get; set; }
            public Node PreviousNode { get; set; }
            public T Value { get; set; }
        }

        private Node head;
        private Node tail;

        public int Count { get; private set; }

        public void AddFirst(T element)
        {
            Node currentNew = new Node(element);

            if (this.Count==0)
            {
                head = tail = currentNew;
            }
            else
            {
                currentNew.NextNode = head;
                head.PreviousNode = currentNew;
                head = currentNew;
            }
            Count++;
        }

        public void AddLast(T element)
        {
            Node currentNew = new Node(element);

            if (Count==0)
            {
                tail = head = currentNew;
            }
            else
            {
                currentNew.PreviousNode = this.tail;
                this.tail.NextNode = currentNew;
                this.tail = currentNew;
            }
            Count++;
        }

        public T RemoveLast()
        {
            if (this.Count==0)
            {
                throw new System.InvalidOperationException("DoublyLinkedList is empty;");
            }
            T toReturn = tail.Value;

            Node previuosNode = tail.PreviousNode;

            if (Count==1)
            {
                tail = head = null;
            }
            else
            {
                previuosNode.NextNode = null;
                tail = previuosNode;
            
            }
            Count--;

            return toReturn;
        }

        public T RemoveFirst()
        {
            if (Count==0)
            {
                throw new System.InvalidOperationException("DoublyLinkedList is empty;");
            }
            T toReturn = head.Value;

            Node currentNew = head.NextNode;

            if (Count==1)
            {
                this.head = this.tail = null;
            }
            else
            {
                currentNew.PreviousNode = null;
                head = currentNew;
            }

            Count--;
            return toReturn;
        }

        public void ForEach(Action<T> action,bool startFromHead=true)
        {
            Node currentNode = head;
            if (!startFromHead)
            {
                currentNode = tail;
            }

            while (currentNode!=null)
            {
                action(currentNode.Value);
                if (startFromHead)
                {
                    currentNode = currentNode.NextNode;
                }
                else
                {
                    currentNode = currentNode.PreviousNode;
                }
            }
        }

        public void Clear()
        {
            this.head = null;
            this.tail = null;
            this.Count = 0;
        }

        public T[] ToArray()
        {
            List<T> arr = new List<T>();

            Node current = head;

            while (head!=null)
            {
                arr.Add(current.Value);
                current = current.NextNode;
            }

            return arr.ToArray();
        }

        public IEnumerator GetEnumerator()
        {
            while (this.head!=null)
            {
                Node current = this.head;
                this.head = this.head.NextNode;
                yield return current.Value;
                
            }
            
        }
    }
}
