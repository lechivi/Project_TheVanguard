using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class NullAwareList<T>
{
    [SerializeField] private List<T> list;

    public NullAwareList()
    {
        this.list = new List<T>();
    }

    public bool ContainsNull()
    {
        for (int i = 0; i < this.list.Count; i++)
        {
            if (this.list[i] == null)
            {
                return true;
            }
        }
        return false;
    }

    public void Add(T child)
    {
        int nullIndex = this.list.FindIndex(item => item == null);
        if (nullIndex != -1)
        {
            this.list[nullIndex] = child;
        }
        else
        {
            this.list.Add(child);
        }
    }

    public void Remove(T child)
    {
        int childIndex = this.list.IndexOf(child);
        this.list.RemoveAt(childIndex);
        this.list.Insert(childIndex, default(T));

        //if (childIndex != -1)
        //{
        //    this.list[childIndex] = default(T);
        //}
    }

    public List<T> GetList()
    {
        return this.list;
    }
}
