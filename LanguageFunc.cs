using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageFunc : MonoBehaviour
{

    Stack<object> stack;
    Queue<object> queue;

    //SortedXXX
    class exampleCompare : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            return -x.CompareTo(y);
        }
    }
    SortedDictionary<int, string> sortedDictionary = new SortedDictionary<int, string>(new exampleCompare());

    [ContextMenu("ssss")]
    public void ssss()
    {

    }

}
