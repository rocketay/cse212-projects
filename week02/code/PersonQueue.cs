/// <summary>
/// A basic implementation of a Queue
/// </summary>
public class PersonQueue
{
    // Index 0 is the front of the queue and the last index is the back
    private readonly List<Person> _queue = new();

    public int Length => _queue.Count;

    /// <summary>
    /// Add a person to the queue
    /// </summary>
    /// <param name="person">The person to add</param>
    public void Enqueue(Person person)
    {
        // Fixed: a new person goes to the back of the queue (end of the list).
        // The original used Insert(0, person), which put them at the front and made
        // the queue behave like a stack (LIFO) instead of a queue (FIFO).
        _queue.Add(person);
    }

    public Person Dequeue()
    {
        // The front of the queue is index 0
        var person = _queue[0];
        _queue.RemoveAt(0);
        return person;
    }

    public bool IsEmpty()
    {
        return Length == 0;
    }

    public override string ToString()
    {
        return $"[{string.Join(", ", _queue)}]";
    }
}

public class Person
{
    public readonly string Name;
    public int Turns { get; set; }

    internal Person(string name, int turns)
    {
        Name = name;
        Turns = turns;
    }

    public override string ToString()
    {
        return Turns <= 0 ? $"({Name}:Forever)" : $"({Name}:{Turns})";
    }
}