using System.Collections.Generic;
using Components;

public class ObservableStack<T> {
    private Stack<T> _stack;
    public delegate void StackChangeHandler();
    public event StackChangeHandler onStackPush;
    public event StackChangeHandler onStackPop;
    public Stack<T> Stack => _stack;
    public int Count => _stack.Count;

    public Stack<T> CreateStack() {
        return _stack = new Stack<T>(); 
    }

    public void Push(T item) {
        if (_stack != null) {
            _stack.Push(item);
            onStackPush?.Invoke();
        }
    }

    public bool TryPop(out T i) {
        if (_stack != null) {
            if (_stack.TryPop(out T item)) {
                onStackPop?.Invoke();
                i = item;
                return true;
            }
        }

        i = default;
        return false;
    }
}