using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    internal class Helper
    {
        public class GlobalTestFixture : IAsyncLifetime
        {
            public Task InitializeAsync()
            {
                // Code to run before all tests
                Console.WriteLine("Global setup before all tests.");
                return Task.CompletedTask;
            }

            public Task DisposeAsync()
            {
                // Code to run after all tests
                Console.WriteLine("Global teardown after all tests.");
                return Task.CompletedTask;
            }
        }
    }
}
