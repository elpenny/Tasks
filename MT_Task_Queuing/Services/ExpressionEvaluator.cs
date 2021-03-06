﻿using MT_Task_Queuing.Interfaces;
using NCalc;
using System;

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
            catch(EvaluationException ex)
            {
                return "Expression result was too big or too small to store it.";
            }
            catch(DivideByZeroException ex)
            {
                return "Expression result was equal to divide by zero operation.";
            }
            

            if (verboseLogging)
            {
                return $"Expression was: {expression} which equals to: {result}";
            }
            return result.ToString();
        }
    }
}