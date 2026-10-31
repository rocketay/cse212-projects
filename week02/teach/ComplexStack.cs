using System;
using System.Collections.Generic;

public static class ComplexStack {
    /// <summary>
    /// Checks that every (, [ and { has a matching closing symbol in the right order.
    /// </summary>
    public static bool DoSomethingComplicated(string line) {
        var stack = new Stack<char>();
        foreach (var item in line) {
            // Opening symbols are pushed so we can match them later (LIFO)
            if (item is '(' or '[' or '{') {
                stack.Push(item);
            }
            // For each closing symbol, the stack must not be empty and the
            // popped symbol must be its matching opening symbol
            else if (item is ')') {
                if (stack.Count == 0 || stack.Pop() != '(')
                    return false;
            }
            else if (item is ']') {
                if (stack.Count == 0 || stack.Pop() != '[')
                    return false;
            }
            else if (item is '}') {
                if (stack.Count == 0 || stack.Pop() != '{')
                    return false;
            }
            // Any other character is ignored
        }

        // Balanced only if every opening symbol was closed (stack is empty)
        return stack.Count == 0;
    }
}