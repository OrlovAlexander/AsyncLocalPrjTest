using System;
using System.Threading;
using System.Threading.Tasks;

class Example
{
    static AsyncLocal<string> _asyncLocalString = new AsyncLocal<string>(Changed);

    static Task AsyncMethodA()
    {
        for (var i = 0; i < 10; i++)
        {
            var ind = i;
            _ = Task.Run(() =>
            {
                _asyncLocalString.Value =
                    $"[{Thread.CurrentThread.ManagedThreadId}] - Value asyncLocalString: '{ind}'";
                Thread.SpinWait(1000);
                Console.WriteLine(
                    $"[{Thread.CurrentThread.ManagedThreadId}] - Value get '{_asyncLocalString.Value}'"
                );
            });
        }

        return Task.CompletedTask;
    }

    private static void AsyncMethodB(string expectedValue)
    {
        Console.WriteLine(
            $"Entering AsyncMethodB - [{Thread.CurrentThread.ManagedThreadId}] - Expected '{expectedValue}', AsyncLocal value is '{_asyncLocalString.Value}'"
        );

        _ = Task.Run(() =>
        {
            Thread.SpinWait(1000);
            Console.WriteLine(
                $"   SubTask - [{Thread.CurrentThread.ManagedThreadId}] - Expected '{expectedValue}', AsyncLocal value is '{_asyncLocalString.Value}'"
            );
        });

        Console.WriteLine(
            $"Exiting AsyncMethodB - [{Thread.CurrentThread.ManagedThreadId}] - Expected '{expectedValue}', got '{_asyncLocalString.Value}'"
        );
    }

    static void Changed(AsyncLocalValueChangedArgs<string> context)
    {
        if (string.IsNullOrWhiteSpace(context.CurrentValue))
        {
            Console.WriteLine(
                $"[{Thread.CurrentThread.ManagedThreadId}] - changed - prev: '{context.PreviousValue}'; curr: '{context.CurrentValue}'"
                    + " - Поток вернулся в пул потоков."
            );
            return;
        }
        Console.WriteLine(
            $"[{Thread.CurrentThread.ManagedThreadId}] - changed - prev: '{context.PreviousValue}'; curr: '{context.CurrentValue}'"
        );
    }

    static async Task Main(string[] args)
    {
        await AsyncMethodA();
        await Task.Delay(5000);

        _asyncLocalString.Value = "Value 1";
        AsyncMethodB("Value 1");
        _asyncLocalString.Value = "Value 2";
        AsyncMethodB("Value 2");

        // Await both calls
        await Task.Delay(5000);

        Console.ReadLine();
    }
}
