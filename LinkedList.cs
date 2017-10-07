using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes {

    public class Node<T> {
        public T Value { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }
    }

    public class LinkedList<T> : IEnumerable<T> {

        Node<T> head;
        Node<T> tail;
        Dictionary<T, Node<T>> dictionary;

        public LinkedList() {
            dictionary = new Dictionary<T, Node<T>>();
        }

        public void Add(T value) {
            if (dictionary.ContainsKey(value) && dictionary[value] != null) return;
            var node = new Node<T>() { Value = value, Previous = tail };
            dictionary[value] = node;
            tail = node;
            if (node.Previous == null) head = tail;
            else node.Previous.Next = node;
        }

        public void ShapeUp(T value) {
            if (dictionary.ContainsKey(value))
                MoveNodeToEnd(dictionary[value]);
        }

        private void MoveNodeToEnd(Node<T> node) {
            if (node.Next == null) return;
            if (node.Previous == null) head = node.Next;
            if (node.Previous != null) node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            node.Next = null;
            node.Previous = tail;
            tail.Next = node;
            tail = node;
        }

        public IEnumerator<T> GetEnumerator() {
            var current = head;
            while (current != null) {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }
}
