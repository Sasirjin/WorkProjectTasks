using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using WorkProjectTasks;

namespace WorkProjectTests
{
    [TestClass]
    public class ThreadingTests
    {
        [TestMethod]
        public void ExecuteInParallel()
        {
            Thread current = Thread.CurrentThread;
            int counter = 0;
            int iterations = 10;

            Action action = () =>
            {
                Interlocked.Increment(ref counter);
                Thread.Sleep(10);

                if (Thread.CurrentThread == current)
                    throw new InvalidOperationException("This delegate should be called from a different thread.");
            };

            ThreadingTasks.ExecuteInParallel(iterations, action);
            Assert.AreEqual(counter, iterations);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ExecuteInParallelWithInvalidArgument()
        {
            ThreadingTasks.ExecuteInParallel(-1, () => { });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ExecuteInParallelWithNullArgument()
        {
            ThreadingTasks.ExecuteInParallel(0, null);
        }
    }
}
