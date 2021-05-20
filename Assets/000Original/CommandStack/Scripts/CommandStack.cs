using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandStack : MonoBehaviour
{
    #region
    // 現状態のスタック番号
    private int currentIndex;
    // currentIndexの調整
    private void adjustIndex()
    {
        if (currentIndex < 0) currentIndex = -1;
        if (currentIndex >= stack.Count) currentIndex = stack.Count - 1;
    }
    #endregion

    public int CurrentIndex
    {
        get => currentIndex;
        private set { currentIndex = value; adjustIndex(); }
    }

    private List<IUndoable> stack;

    private void Awake()
    {
        stack = new List<IUndoable>();
        adjustIndex();
    }

    private bool isOutOfRange(int index)
    {
        return index < 0 || stack.Count <= index;
    }

    public void Add(IUndoable undoable)
    {
        int removingIndex = CurrentIndex + 1;
        if(!isOutOfRange(removingIndex)) stack.RemoveRange(removingIndex, stack.Count - removingIndex);
        stack.Add(undoable);
        CurrentIndex++;
    }

    public bool Undo()
    {
        // 戻り先のなしは無効
        if (CurrentIndex < 0) return false;

        stack[CurrentIndex].Undo();
        CurrentIndex--;
        return true;
    }

    public bool Redo()
    {
        // やり直し先のなしは無効
        if (CurrentIndex + 1 >= stack.Count) return false;

        CurrentIndex++;
        stack[CurrentIndex].Redo();
        return true;
    }
}
