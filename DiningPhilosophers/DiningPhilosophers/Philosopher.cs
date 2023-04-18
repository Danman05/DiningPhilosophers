
namespace DiningPhilosophers;

public class Philosopher
{
    // Fields

    public int Id;
    private int rightFork;
    private int leftFork;

    // Constructor for the Philosopher object, This constructor takes in an id
    public Philosopher(int id)
    {
        Id = id;

        // If the id from the constructor is 0, then the leftFork gets the last index of the fork array.
        // Otherwise leftfork would be -1 and that would result in index out of bounds
        if (id == 0)
        {
            rightFork = 0;
            leftFork = 4;
        }
        else
        {
            rightFork = Id;
            leftFork = Id - 1;
        }
    }

    public void Do(object callback)
    {
        while (true)
        {
            Think();
            Wait();
        }
    }

    /// <summary>
    /// Makes the philosopher wait until he can eat.                                                                                                                                    
    /// </summary>
    private void Wait()
    {
        int tries = 0;
        bool ate = false;
        Console.WriteLine("Philosopher{0} is waiting", Id);
        while (!ate)
        {
            // Try to eat, right and left fork is required to eat
            // If only the first statement succeeds then Moniter.Exit the right fork again
            try
            {
                if (Monitor.TryEnter(Program.forks[rightFork]))
                {
                    if (Monitor.TryEnter(Program.forks[leftFork]))
                    {
                        Console.WriteLine("Philosopher{0} tried {1} times before eating", Id, tries);
                        tries = 0;
                        ate = true;
                        Eat();
                        Monitor.Exit(Program.forks[leftFork]);
                    }
                    Monitor.Exit(Program.forks[rightFork]);
                }
                tries++;
            }
            finally
            {
                if (tries == 100)
                {
                    Dead();
                }
                Thread.Sleep(100);
            }

        }
    }

    /// <summary>
    /// Method to "Remove" an philosopher
    /// </summary>
    private void Dead()
    {
        Console.WriteLine("Philosopher{0} is DEAD", Id);
        while (true)
        {
            // The Abort method is obsolete and will not work
            //Thread.CurrentThread.Abort();

            Thread.Sleep(100000);
        }
    }


    /// <summary>
    /// Random amount of time to eat
    /// </summary>
    private void Eat()
    {
        Console.WriteLine("Philosopher{0} is eating", Id);
        Random r = new Random();
        Thread.Sleep(r.Next(500, 2000));
    }

    /// <summary>
    /// Random amount of time to think
    /// </summary>
    private void Think()
    {
        Console.WriteLine("Philosopher{0} is thinking", Id);
        Random r = new Random();
        Thread.Sleep(r.Next(500, 2000));
    }
}