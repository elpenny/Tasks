using System;

namespace MT_Task_Queuing
{
    internal class TaskGenerator
    {
        private string[] expressions = new string[30]{"1+1","2+2","3+3","4+4","5+5","6+6","7+7","8+8","9+9","10+10", "11+11", "12+12", "13+13", "14+14", "15+15", "16+16", "17+17", "18+18", "19+19", "20+20", "21+21" , "22+22" , "23+23" , "24+24" , "25+25" , "26+26" , "27+27" , "28+28" , "29+29" , "30+30" };
        private int _iterator = 0;

        private Random _random = new Random();

        public string Next()
        {
            //return new string(_random.Next().ToString() + "+" + _random.Next().ToString());
            return expressions[_iterator++];
        }
    }
}