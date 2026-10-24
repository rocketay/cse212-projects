public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // PLAN:
        // 1. Create a new array of doubles with size 'length'.
        // 2. Use a for loop with index i going from 0 to length - 1.
        // 3. In each iteration, the multiple is 'number' times (i + 1),
        //    because index 0 must hold the first multiple (number * 1),
        //    index 1 holds number * 2, and so on.
        // 4. Store that value in the array at position i.
        // 5. After the loop ends, return the array.

        double[] multiples = new double[length];

        for (int i = 0; i < length; i++)
        {
            multiples[i] = number * (i + 1);
        }

        return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // PLAN:
        // 1. The last 'amount' items of the list must move to the front,
        //    keeping their order. The remaining items follow after them.
        // 2. Calculate the index where the last 'amount' items start:
        //    startIndex = data.Count - amount.
        // 3. Use GetRange(startIndex, amount) to copy those last items
        //    into a new temporary list (the "tail").
        // 4. Use RemoveRange(startIndex, amount) to delete those items
        //    from the end of the original list.
        // 5. Use InsertRange(0, tail) to put the saved items at the
        //    beginning of the original list.
        // 6. If amount equals data.Count, startIndex is 0, so the tail is the
        //    whole list; removing and reinserting it leaves the list unchanged,
        //    which is the correct result.

        int startIndex = data.Count - amount;

        List<int> tail = data.GetRange(startIndex, amount);
        data.RemoveRange(startIndex, amount);
        data.InsertRange(0, tail);
    }
}