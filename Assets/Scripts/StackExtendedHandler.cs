using System.Collections.Generic;
using Components;

public class StackExtendedHandler<T> {
    private Stack<T> _stack;
    public delegate void StackChangeHandler(StackComponent stackComponent);
    public event StackChangeHandler onStackChanged;
    public Stack<T> Stack => _stack;

    public Stack<T> CreateStack() {
        return _stack = new Stack<T>();
    }

    public void Push(T item, StackComponent stackComponent) {
        if (_stack != null) {
            _stack.Push(item);
            onStackChanged?.Invoke(stackComponent);
        }
    }

    public T Pop(StackComponent stackComponent) {
        if (_stack != null) {
            if (_stack.TryPop(out T item)) {
                onStackChanged?.Invoke(stackComponent);
                return item;
            }
        }
        return default(T);
    }
}