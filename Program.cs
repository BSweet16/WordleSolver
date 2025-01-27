using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Create an instance of the WordleSolver
        WordleSolver solver = new WordleSolver();

        // Run the solver
        await solver.RunSolver();
    }
}