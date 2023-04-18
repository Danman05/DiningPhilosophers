/*
 *  Delt kode med Niklas og Emil
 *  Kode kommentar er ikke delt
*/ 


using DiningPhilosophers;

public class Program
{

    // Static array that holds 5 Forks
    public static readonly Fork[] forks = new Fork[5] { new Fork(), new Fork(), new Fork(), new Fork(), new Fork() };

    // Static array that holds 5 Philosophers
    static readonly Philosopher[] philosophers = new Philosopher[5];
    public static void Main()
    {

        // Makes 5 philosophers that gets assigned an id from the for-loop
        // The "phil" object gets assigned into the [i] space in the Philosopher array
        for (int i = 0; i < 5; i++)
        {
            Philosopher phil = new Philosopher(i);
            philosophers[i] = phil;
        }

        // ThreadPool for each philosopher in the array and makes them execute the Do method in the philosopher class
        for (int i = 0; i < philosophers.Length; i++)
        {
            ThreadPool.QueueUserWorkItem(philosophers[i].Do);
        }

        Console.Read();
    }
}