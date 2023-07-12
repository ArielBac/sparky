namespace Sparky;

public interface ILogBook
{
    public int LogSeverity { get; set; }
    public string LogType { get; set; }
    void Message(string message);
    bool LogToDb(string message);
    bool LogBalanceAfterWithdrawal(int balanceAfaterWithdrawal);
    string MessageWithReturnStr(string message);
    bool LogWithOutputResult(string str, out string outputStr);
    bool LogWithRwfObj(ref Customer customer);
}
public class LogBook : ILogBook
{
    public int LogSeverity { get ; set; }
    public string LogType { get; set; }

    public bool LogBalanceAfterWithdrawal(int balanceAfaterWithdrawal)
    {
        if (balanceAfaterWithdrawal >= 0)
        {
            Console.WriteLine("Success");
            return true;
        }

        Console.WriteLine("Failure");
        return false;
    }

    public bool LogToDb(string message)
    {
        Console.WriteLine(message);
        return true;
    }

    public bool LogWithOutputResult(string str, out string outputStr)
    {
        outputStr = "Hello " + str;
        return true;
    }

    public bool LogWithRwfObj(ref Customer customer)
    {
        return true;
    }

    public void Message(string message)
    {
        Console.WriteLine(message);
    }

    public string MessageWithReturnStr(string message)
    {
        Console.WriteLine(message);
        return message.ToLower();
    }
}

//public class LogFakker : ILogBook
//{
//    public void Message(string message)
//    {
//    }
//}
