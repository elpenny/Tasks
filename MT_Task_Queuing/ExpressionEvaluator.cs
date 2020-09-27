using System;
using System.Data;

namespace MT_Task_Queuing
{
    internal class ExpressionEvaluator
    {
        public string Anaylyze(string expression, bool verboseLogging)
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