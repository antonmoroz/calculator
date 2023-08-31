using Calculator.Enums;
using System;

namespace Calculator.Models
{
    class CalculatorModel
    {
        private float operand1;

        private float operand2;

        public CalculatorModel()
        {
            Operation = Operation.None;
        }

        public float Operand
        {
            get
            {
                return (Operation == Operation.None) ? operand1 : operand2;
            }
            set
            {
                if (Operation == Operation.None)
                {
                    operand1 = value;
                }
                else
                {
                    operand2 = value;
                }
            }
        }

        public float Result { get; set; }

        public Operation Operation { get; set; }

        public float MemoryValue { get; private set; }

        public void DoOperation()
        {
            switch (Operation)
            {
                case Operation.Add:
                    Add();
                    break;
                case Operation.Subtract:
                    Subtract();
                    break;
                case Operation.Multiply:
                    Multiply();
                    break;
                case Operation.Divide:
                    Divide();
                    break;
                case Operation.Inverse:
                    Inverse();
                    break;
                default:
                    break;
            }

            Operation = Operation.None;
        }

        public void Percent()
        {
            operand2 = operand1 / 100 * operand2;
        }

        public void Inverse()
        {
            Operand = 1 / Operand;
        }

        public void SquareRoot()
        {
            Operand = (float)Math.Sqrt(Operand);
        }

        public void MemoryPlus(float value)
        {
            MemoryValue += value;
        }

        public void MemoryMinus(float value)
        {
            MemoryValue -= value;
        }

        public void MemoryStore(float value)
        {
            MemoryValue = value;
        }

        public void MemoryClear()
        {
            MemoryValue = 0;
        }

        public void ClearEntry()
        {
            Operand = 0;
        }

        public void Clear()
        {
            operand1 = 0;
            operand2 = 0;
            Operation = Operation.None;
        }

        private void Add()
        {
            Result = operand1 + operand2;
        }

        private void Subtract()
        {
            Result = operand1 - operand2;
        }

        private void Multiply()
        {
            Result = operand1 * operand2;
        }

        private void Divide()
        {
            Result = operand1 / operand2;
        }
    }
}