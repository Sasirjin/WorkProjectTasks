namespace WorkProjectTasks
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class ThreadingTasks
    {
        /// <summary>
        /// This method should execute the <paramref name="action"/> the number of specified <paramref name="executions"/> and wait until actions have completely executed.
        /// </summary>
        /// <param name="executions">The number of times to execute the specified <paramref name="action"/>.</param>
        /// <param name="action">The action to execute.</param>
        /// <exception cref="ArgumentException">The number of <paramref name="executions"/> must be greater-than or equal to 0.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="action"/> is null.</exception>
        public static void ExecuteInParallel(int executions, Action action)
        {
            // Test input parameters         
            if (action == null)
            {
                throw new ArgumentNullException("action", "The action is null.");
            }

            if (executions < 1)
            {
                throw new ArgumentException("The number of executions must be greater-than or equal to 0.", "executions");
            }

            // Create list of task<actions>
            var tasks = new List<Task>();

            // Fill list of tasks
            for (int exec = 0; exec < executions; exec++)
            {
                tasks.Add(Task.Factory.StartNew(action));
            }

            // Wait for all the tasks to finish.
            Task.WaitAll(tasks.ToArray());
        }
    }
}