using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
//https://gist.github.com/hearsid/807b918cb3c8edfc116cad362caffc02

namespace AStar
{
    class PriorityQueue<T>
    {
           int total_size;
        SortedDictionary<int, Queue<T>> storage;

        public PriorityQueue()
        {
            
            
            this.storage = new SortedDictionary<int, Queue<T>> ();
            this.total_size = 0;
        }

        public bool IsEmpty ()
        {
            return (total_size == 0);
        }

        public T Dequeue ()
        {
            if (IsEmpty ()) {
                throw new Exception ("Please check that priorityQueue is not empty before dequeing");
            } else
                foreach (Queue<T> q in storage.Values) {
                    // we use a sorted dictionary
                    if (q.Count > 0) {
                        total_size--;
                        return  q.Dequeue ();
                    }
                }

                Debug.Assert(false,"not supposed to reach here. problem with changing total_size");

                return default (T); // not supposed to reach here.
        }

        // same as above, except for peek.

        public T Peek ()
        {
            if (IsEmpty ())
                throw new Exception ("Please check that priorityQueue is not empty before peeking");
            else
                foreach (Queue<T> q in storage.Values) {
                    if (q.Count > 0)
                        return q.Peek ();
                }

                Debug.Assert(false,"not supposed to reach here. problem with changing total_size");

                return default(T); // not supposed to reach here.
        }

        public T Dequeue (int prio)
        {
            total_size--;
            return storage[prio].Dequeue ();
        }

        public void Enqueue (T item, int prio)
        {
            if (!storage.ContainsKey (prio)) {
                storage.Add (prio, new Queue<T> ());
              }
            storage[prio].Enqueue (item);
            total_size++;

        }

    }
}
