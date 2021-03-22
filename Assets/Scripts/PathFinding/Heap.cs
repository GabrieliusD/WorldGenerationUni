using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Heap<T> where T : IHeapItem<T>
{
    T[] items;
    int currentItemCount;

    public Heap(int maxHeapSize)
    {
        items = new T[maxHeapSize];
    }
    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }
    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentItemCount--;
        items[0] = items[currentItemCount];
        items[0].HeapIndex = 0;
        SortDown(items[0]);
        return firstItem;
    }
    public void UpdateItem(T item)
    {
        SortUp(item);
    }
    public int Count
    {
        get { return currentItemCount; }
    }
    public bool Contains(T item)
    {
        return Equals(items[item.HeapIndex], item);
    }
    void SortDown(T item)
    {
        while (true)
        {
            ChildIndex childIndex = GetChildIndex(item);
            int leftChildIndex = childIndex.left;
            int rightChildIndex = childIndex.right;
            int swapIndex = 0;

            if (leftChildIndex < currentItemCount)
            {
                swapIndex = leftChildIndex;
                if (rightChildIndex < currentItemCount)
                {
                    if (items[leftChildIndex].CompareTo(items[rightChildIndex]) < 0)
                    {
                        swapIndex = rightChildIndex;
                    }
                }
                if (item.CompareTo(items[swapIndex]) < 0)
                {
                    Swap(item, items[swapIndex]);
                }
                else
                {
                    return;
                }
            }
            else
            {
                return;
            }
        }
    }
    void SortUp(T item)
    {
        int parentIndex = GetParentIndex(item);
        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
            {
                Swap(item, parentItem);
            }
            else 
            {break;}

            parentIndex = GetParentIndex(item);
        }

    }
    int GetParentIndex(T item)
    {
        return (item.HeapIndex - 1) / 2;
    }
    ChildIndex GetChildIndex(T item)
    {
        int left = item.HeapIndex * 2 + 1;
        int right = item.HeapIndex * 2 + 2;
        return new ChildIndex(left, right);
    }
    void Swap(T itemA, T itemB)
    {
        items[itemA.HeapIndex] = itemB;
        items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    int HeapIndex
    {
        get;
        set;
    }
}

struct ChildIndex
{
    public int left;
    public int right;

    public ChildIndex(int _left, int _right)
    {
        left = _left;
        right = _right;
    }
}