using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Lab5
{

    [Serializable]
    public class DblQueue<T> : ICollection<T>, ICloneable
    {

        // coada dubla este simulata prin List<T>  
        protected List<T> internalList = null;

        public DblQueue()
        {
            internalList=new List<T>();
        }

        public DblQueue(ICollection<T> coll)
        {
            int i = 0;

            internalList = new List<T>(coll.Count);
            foreach (var it in coll)
                internalList[i++] = it;
        }

        // numar elemente din coada (tip Read)  
        public virtual int Count
        {
            get { return internalList.Count; }
        }

        public virtual bool IsSynchronized
        {
            get { return (false); }
        }

        public virtual object SyncRoot
        {
            get { return (this); }
        }

        // golirea cozii  
        public virtual void Clear()
        {
            internalList = new List<T>();
        }

        // Crearea unei noi cozi duble  
        public object Clone()
        {
            return this;
        }

        // Coada contine obj?  
        public virtual bool Contains(T obj)
        {
            return internalList.Contains(obj);
        }

        // copiere coada in Array incepand cu index  
        public virtual void CopyTo(Array array, int index)
        {
            for (int i = 0; i < array.Length; i++)
                if (index < Count)
                    array.SetValue(internalList[index++], i);
                else
                    return;
        }

        // scoaterea unui obiect din coada folosind LIFO  
        public virtual T DequeueHead()
        {
            T x=internalList[0];
            internalList.RemoveAt(0);
            return x;
        }

        // scoaterea unui obiect din coada folosind FIFO  
        public virtual T DequeueTail()
        {
            T x = internalList[Count-1];
            internalList.RemoveAt(Count-1);
            return x;
        }

        // Introducerea unui obiect in coada la inceputul cozii  
        public virtual void EnqueueHead(T obj)
        {
            //internalList.Add
            List<T> x = new List<T>();
            x.Add(obj);
            for (int i = 0; i < Count; i++)
                x.Add(internalList[i]);
            internalList = x;

        }

        // Introducerea unui obiect in coada la sfarsitul cozii  
        public virtual void EnqueueTail(T obj)
        {
            internalList.Add(obj);
        }

        // verificarea primului element din coada (metoda Peek)  
        public virtual T PeekHead()
        {
            return internalList[0];
        }

        // verificarea ultimului element din coada (metoda Peek)  
        public virtual T PeekTail()
        {
            return internalList[Count - 1];
        }

        // enumerator   
        public virtual IEnumerator GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        // coada transformata intr-un array  
        public virtual T[] ToArray()
        {
            T[] x=new T[Count];
            for (int i = 0; i < Count; i++)
                x[i] = internalList[i];
            return x;
        }

        // eliminare spatiu in exces din coada   
        public virtual void TrimExcess()
        {
        }

        void System.Collections.Generic.ICollection<T>.Add(T item)
        {
            throw (new NotSupportedException("Use the EnqueueHead or EnqueueTail methods."));
        }

        // Copiere in array a cozii incepand cu index  
        void System.Collections.Generic.ICollection<T>.CopyTo(T[] item, int index)
        {
            for (int i = 0; i < item.Length; i++)
                if (index < Count)
                    item.SetValue(internalList[index++], i);
                else
                    return;
        }

        bool System.Collections.Generic.ICollection<T>.Remove(T item)
        {
            throw (new NotSupportedException("Use the DequeueHead or DequeueTail methods."));
        }

        bool System.Collections.Generic.ICollection<T>.IsReadOnly
        {
            get { throw (new NotSupportedException("Not Supported.")); }
        }

        // enumerator   
        IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        public void afis()
        {
            Console.WriteLine();
            foreach (var it in internalList)
                Console.Write(it + "  ");
            Console.WriteLine();
        }
        
    } 
}
