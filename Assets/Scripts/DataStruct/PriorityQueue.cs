using System;
using System.Collections.Generic;

//基于堆的优先队列
public class PriorityQueue<T> where T : IComparable<T> {
    private List<T> heap;
    private bool isMinHeap;

    public PriorityQueue() : this(true)
    {

    }


    public PriorityQueue(bool isMinHeap = true) {
        this.heap = new List<T>();
        this.isMinHeap = isMinHeap;
    }

    public int Count {
        get { return heap.Count; }
    }

    public void Enqueue(T item) {
        heap.Add(item);
        HeapifyUp(heap.Count - 1);
    }

    public T Dequeue() {
        if (heap.Count == 0) {
            throw new InvalidOperationException("Priority queue is empty");
        }

        T firstItem = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        HeapifyDown(0);

        return firstItem;
    }

    private void HeapifyUp(int index) {
        int parentIndex = (index - 1) / 2;

        if ((isMinHeap && heap[parentIndex].CompareTo(heap[index]) > 0) ||
            (!isMinHeap && heap[parentIndex].CompareTo(heap[index]) < 0)) {
            Swap(parentIndex, index);
            HeapifyUp(parentIndex);
        }
    }

    private void HeapifyDown(int index) {
        int leftChildIndex = 2 * index + 1;
        int rightChildIndex = 2 * index + 2;
        int swapIndex = index;

        if (leftChildIndex < heap.Count &&
            ((isMinHeap && heap[leftChildIndex].CompareTo(heap[index]) < 0) ||
            (!isMinHeap && heap[leftChildIndex].CompareTo(heap[index]) > 0))) {
            swapIndex = leftChildIndex;
        }

        if (rightChildIndex < heap.Count &&
            ((isMinHeap && heap[rightChildIndex].CompareTo(heap[swapIndex]) < 0) ||
            (!isMinHeap && heap[rightChildIndex].CompareTo(heap[swapIndex]) > 0))) {
            swapIndex = rightChildIndex;
        }

        if (swapIndex != index) {
            Swap(swapIndex, index);
            HeapifyDown(swapIndex);
        }
    }

    private void Swap(int indexA, int indexB) {
        (heap[indexA], heap[indexB]) = (heap[indexB], heap[indexA]);
    }

    public void Clear()
    {
        heap.Clear();
    }
}