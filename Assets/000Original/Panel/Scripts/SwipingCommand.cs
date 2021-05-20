using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipingCommand : IUndoable
{
    private System.Action undoing;
    private System.Action redoing;

    public SwipingCommand(System.Action undoing, System.Action redoing)
    {
        this.undoing = undoing;
        this.redoing = redoing;
    }

    public void Undo()
    {
        undoing?.Invoke();
    }

    public void Redo()
    {
        redoing?.Invoke();
    }

}
