namespace MT_Task_Queuing.Interfaces
{
    internal interface IExpressionEvaluator
    {
        string Execute(string expression, bool verboseLogging);
    }
}