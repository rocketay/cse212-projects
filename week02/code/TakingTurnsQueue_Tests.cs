using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue one item ("A", priority 1), dequeue it, and then look at the queue.
    // Expected Result: Dequeue returns "A" and the queue is empty afterwards: "[]".
    // Defect(s) Found: The test failed. The queue still contained "A (Pri:1)" after
    // Dequeue because the original code returned the value but never removed
    // the item from the list. Fixed by adding RemoveAt(highPriorityIndex).
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());
        Assert.AreEqual("[]", priorityQueue.ToString());
    }

    [TestMethod]
    // Scenario: Enqueue A (1), B (5), C (3). Dequeue once.
    // Expected Result: "B", because it has the highest priority and it is in the middle of the queue.
    // Defect(s) Found: None. The first Dequeue returned the right value with the original code.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        Assert.AreEqual("B", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A (1), B (2), C (9). The highest priority is the LAST item. Dequeue once.
    // Expected Result: "C"
    // Defect(s) Found: The test failed. It returned "B" instead of "C". The loop in Dequeue used
    // index < _queue.Count - 1, so the last item in the queue was never checked.
    // Fixed by changing the condition to index < _queue.Count.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 9);

        Assert.AreEqual("C", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A (3), B (3), C (1). A and B have the same highest priority. Dequeue once.
    // Expected Result: "A", because it is closest to the front of the queue (FIFO for ties).
    // Defect(s) Found: The test failed. It returned "B" instead of "A". The comparison used >=,
    // so the later item with the same priority replaced the earlier one.
    // Fixed by changing >= to >.
    public void TestPriorityQueue_4()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 3);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 1);

        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue A (1), B (5), C (3), D (5). Dequeue until the queue is empty.
    // Expected Result: B, D, C, A (highest priority first, ties in FIFO order).
    // Defect(s) Found: The test failed on the second Dequeue: "B" was returned again instead
    // of "D" because the item was never removed from the queue. Fixed by removing the item
    // in Dequeue (the same defect found in TestPriorityQueue_1).
    public void TestPriorityQueue_5()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);
        priorityQueue.Enqueue("D", 5);

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue from an empty queue.
    // Expected Result: InvalidOperationException with the message "The queue is empty."
    // Defect(s) Found: None. This test passed with the original code because the empty
    // check and the error message were already correct.
    public void TestPriorityQueue_6()
    {
        var priorityQueue = new PriorityQueue();

        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Enqueue A (5) and then B (1). Look at the queue without dequeuing.
    // Expected Result: "[A (Pri:5), B (Pri:1)]", because Enqueue always adds to the back
    // regardless of priority.
    // Defect(s) Found: None. This test passed with the original code.
    public void TestPriorityQueue_7()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 1);

        Assert.AreEqual("[A (Pri:5), B (Pri:1)]", priorityQueue.ToString());
    }
}