using MT_Task_Queuing.Interfaces;
using System;
using NCalc;

namespace MT_Task_Queuing.Services
{
    public class ExpressionEvaluator : IExpressionEvaluator
    {
        public string Execute(string expression, bool verboseLogging)
        {
            double result;

            try
            {
                result = new Expression(expression).ToLambda<double>()();
            }
            catch (OverflowException ex)
            {
                return "Expression result was too big or too small to store it.";
            }
            

            if(verboseLogging)
            {
                return $"Expression was: {expression} which equals to: {result}";
            }
            return result.ToString();
        }
    }
}