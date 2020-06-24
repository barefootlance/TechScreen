using System;
using System.Collections.Generic;

namespace Library
{
    // 3. Mystery Method               
    // Describe what the Mystery method does and discuss any potential bugs and possible fixes.
    public class P
    {
        // TODO: these should be private, read-only properties, not public variables
        public string Name;
        public P[] Acquaintances;

        public P(string name, P[] acquaintances)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or white space.", "name");
            }

            this.Name = name;
            this.Acquaintances = acquaintances; // TODO: note that it is acceptable for the array to be null or empty, unlike the name
        }

        // Recursively searches a network of acquaintances to see if there is someone with that name in the network.
        public bool Mystery(string name)
        {
            // TODO: this check is not strictly necessary, because the Equals test below will simply fail to match anything. 
            // TODO: However, if it is intended to be the same check as in the constructor then the code in both places should be
            // TODO: replaced with a call to a validation method that contains this check so that the two don't get out of sync.
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be null or white space.", "name");
            }

            // TODO: initialize a stack with this person's acquaintances
            var myStack = new Stack<P>();
            // TODO: the initialization should be in the call to the constructor (when it is not null!), not in a separate loop
            foreach (P acquaintance in this.Acquaintances) // TODO: this will throw and exception if Acquaintances is null
            {
                myStack.Push(acquaintance);
            }

            do
            {
                // TODO: take the first acquaintance off the stack
                var person = myStack.Pop(); // TODO: this will throw an exception if the stack is empty

                // TODO: if this is the droid, er, person you're looking for, return that we found them,
                // TODO: (or at least we found someone with that name, there may be more than one).
                // TODO: That is, the name passed in is in the network of friends of the person that this
                // TODO: object represents.
                if (person.Name.Equals(name))
                {
                    return true;
                }

                // TODO: expand the network by adding the acquaintances of this acquaintance
                // TODO: In general, this will not end because people will be added to the stack
                // TODO: again even if we've already checked them against the name that is passed in.
                // TODO: The most obvious example is the person that this P object represents - because
                // TODO: acquaintances will likely be symmetric (if A is an acquaintance of B, B will
                // TODO: be an acquaintance of A), so this person will get pushed onto the stack by
                // TODO: their acquaintance (this.Name won't be on the stack). But, of course, then
                // TODO: we'll start processing this.Acquaintances again because the acquaintance
                // TODO: we're processing now will no longer be on the stack. We should not push
                // TODO: an acquaintance onto the stack if they're already there, but more importantly
                // TODO: we need to track the acquaintances we've already processed, so that we don't
                // TODO: get caught in a cycle.
                foreach (P acquaintance in person.Acquaintances)
                {
                    myStack.Push(acquaintance);
                }

            } while (myStack.Count>= 0);   // TODO: to avoid the empty stack exception this test should be at the top of the loop

            return false;
        }
    }
}