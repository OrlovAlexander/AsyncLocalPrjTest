using System;
using System.Threading;
using System.Threading.Tasks;

class Example
{
    private static readonly AsyncLocal<string> AsyncLocalString = new AsyncLocal<string>(Changed);

    /// <summary>
    /// Записывает и считывает некоторую строку в/из AsyncLocalString в неупорядоченных тасках.
    /// Всего запускается 10 тасок.
    /// Таски запускаются в цикле и не ждут окончания друг друга.
    /// Нет возможности предсказать запись и чтение в/из AsyncLocalString.
    /// </summary>
    static Task AsyncMethodA()
    {
        for (var i = 0; i < 10; i++)
        {
            var ind = i;
            _ = Task.Run(() =>
            {
                var value = $"'Value{ind}'";

                Console.WriteLine($"В AsyncLocalString будет записана строка: {value}");
                
                AsyncLocalString.Value = $"{value}";
                Thread.SpinWait(1000);

                Console.WriteLine($"Из AsyncLocalString получена строка: '{AsyncLocalString.Value}'"
                );
            });
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Этот метод вызывается из основного потока.
    /// В основном потоке устанавливается и затем меняется значение AsyncLocalString.
    /// В этом методе выполняется только чтение значения AsyncLocalString
    /// </summary>
    private static void AsyncMethodB(string expectedValue)
    {
        Console.WriteLine($"AsyncMethodB Entering - " +
                          $"Expected '{expectedValue}', AsyncLocal value is '{AsyncLocalString.Value}'"
        );

        _ = Task.Run(() =>
        {
            Thread.SpinWait(1000);
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] - " +
                              $"SubTask - " +
                              $"Expected '{expectedValue}', AsyncLocal value is '{AsyncLocalString.Value}'"
            );
        });
        
        Console.WriteLine($"AsyncMethodB Exiting - " +
                          $"Expected '{expectedValue}', AsyncLocal value is '{AsyncLocalString.Value}'"
        );
    }

    /// <summary>
    /// Вызывается runtime при изменении значения AsyncLocalString
    /// </summary>
    /// <param name="context"></param>
    static void Changed(AsyncLocalValueChangedArgs<string> context)
    {
        if (string.IsNullOrWhiteSpace(context.CurrentValue))
        {
            Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] - " +
                              $"Changed - " +
                              $"'{context.PreviousValue}' => '{context.CurrentValue}' - " +
                              $"Поток вернулся в пул потоков."
            );
            return;
        }
        Console.WriteLine(
            $"[{Thread.CurrentThread.ManagedThreadId}] - " +
            $"Changed - '{context.PreviousValue}' => '{context.CurrentValue}'"
        );
    }

    static async Task Main(string[] args)
    {
        // Вариант А
        // await AsyncMethodA();
        // await Task.Delay(5000);
        // --------------------------------------------------------------

        // Вариант В
        AsyncLocalString.Value = "Value 1";
        AsyncMethodB("Value 1");
        Thread.SpinWait(500);
        AsyncLocalString.Value = "Value 2";
        // Await both calls
        await Task.Delay(5000);
        Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] - " +
                          $"Main - " +
                          $"Expected 'Value 2', AsyncLocal value is '{AsyncLocalString.Value}'");
        // --------------------------------------------------------------

        Console.ReadLine();
    }
}
