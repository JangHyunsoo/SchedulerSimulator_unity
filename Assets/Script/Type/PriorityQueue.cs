using System;
using System.Collections;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    List<T> nowList;
    IComparer<T> comparer;

    public PriorityQueue()
    {
        nowList = new List<T>();
    }

    public PriorityQueue(IComparer<T> _comparer)
    {
        nowList = new List<T>();
        comparer = _comparer;
    }

    public void push(T data)
    {
        int count = nowList.Count;
        int index = -1;
        if (count < 1)
        {
            nowList.Add(data);
            return;
        }
        for (int i = count; --i > -1;)
            if (comparer.Compare(nowList[i], data) > 0)
            {
                index = i + 1;
                break;
            }
        if (index.Equals(-1))
            index = 0;
        nowList.Insert(index, data);
    }

    public T pop()
    {
        T g = nowList[0];
        nowList.RemoveAt(0);
        return g;
    }

    public T peek()
    {
        return nowList[0];
    }

    public void clear()
    {
        nowList.Clear();
    }

    public void resort()
    {
        nowList.Sort(comparer);
    }

    public int count
    {
        get
        {
            return nowList.Count;
        }
    }

    public bool empty
    {
        get
        {
            return nowList.Count == 0;
        }
    }

    public T[] ToArray()
    {
        return nowList.ToArray();
    }
}
