using System.Collections.Generic;

namespace EMS
{
    public static class UndoRedoManager
    {
        private static Stack<IUndoableCommand> undoStack =
            new Stack<IUndoableCommand>();

        private static Stack<IUndoableCommand> redoStack =
            new Stack<IUndoableCommand>();

        // EXECUTE
        public static void Execute(IUndoableCommand command)
        {
            command.Execute();

            undoStack.Push(command);

            redoStack.Clear();
        }

        // UNDO
        public static void Undo()
        {
            if (undoStack.Count == 0)
                return;

            IUndoableCommand command =
                undoStack.Pop();

            command.Undo();

            redoStack.Push(command);
        }

        // REDO
        public static void Redo()
        {
            if (redoStack.Count == 0)
                return;

            IUndoableCommand command =
                redoStack.Pop();

            command.Execute();

            undoStack.Push(command);
        }

        // CLEAR
        public static void Clear()
        {
            undoStack.Clear();
            redoStack.Clear();
        }
    }
}