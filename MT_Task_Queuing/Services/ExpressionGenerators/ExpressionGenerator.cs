using MT_Task_Queuing.Interfaces;
using System;
using System.Text;

namespace MT_Task_Queuing.Services.ExpressionGenerators
{
    public class ExpressionGenerator : IExpressionGenerator
    {
        private Random _random;
        private int _maxNumberOfOperations;
        private int _maxValue;

        public ExpressionGenerator(int seed, IConfiguration configuration)
        {
            _random = new Random(seed);
            _maxNumberOfOperations = configuration.TaskOperationsMaxCount;
            _maxValue = configuration.TaskNumbersMaxValue;
        }

        public string GenerateExpression()
        {
            StringBuilder builder = new StringBuilder();

            int numOfOperand = _random.Next(1, _maxNumberOfOperations); 
            int randomNumber;
            for (int i = 0; i < numOfOperand; i++)
            {

                randomNumber = _random.Next(1, _maxValue);
                builder.Append(randomNumber);


                int randomOperand = _random.Next(1, 4);

                string operand = null;

                switch (randomOperand)
                {
                    case 1:
                        operand = "+";
                        break;
                    case 2:
                        operand = "-";
                        break;
                    case 3:
                        operand = "*";
                        break;
                    case 4:
                        operand = "/";
                        break;
                }
                builder.Append(operand);
            }
            randomNumber = _random.Next(1, _maxValue);
            builder.Append(randomNumber);

            return builder.ToString();
        }
    }
}
