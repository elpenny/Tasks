using MT_Task_Queuing.Interfaces;
using System;
using System.Data;

namespace MT_Task_Queuing.Services
{
    internal class ExpressionEvaluator : IExpressionEvaluator
    {
        public string Execute(string expression, bool verboseLogging)
        {
            double result = Convert.ToDouble(new DataTable().Compute(expression, null));

            if(verboseLogging)
            {
                return $"Expression was: {expression} which equals to: {result}";
            }
            return result.ToString();
        }
    }
}