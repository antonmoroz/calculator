using Calculator.Commands;
using Calculator.Enums;
using Calculator.Models;
using Calculator.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private const int MaxNumberLength = 18;

        private string displayedNumber;

        private bool resetNumber;

        private readonly CalculatorModel calculatorModel;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindowViewModel()
        {
            displayedNumber = "0";
            resetNumber = true;
            InsertDigitOrDotCommand = new RelayCommand<string>(InsertDigitOrDot);
            AddOperationCommand = new RelayCommand(AddOperation);
            SubtractOperationCommand = new RelayCommand(SubtractOperation);
            MultiplyOperationCommand = new RelayCommand(MultiplyOperation);
            DivideOperationCommand = new RelayCommand(DivideOperation);
            InverseOperationCommand = new RelayCommand(InverseOperation);
            PercentOperationCommand = new RelayCommand(PercentOperation);
            BackspaceOperationCommand = new RelayCommand(BackspaceOperation);
            SquareRootOperationCommand = new RelayCommand(SquareRootOperation);
            ChangeSignOperationCommand = new RelayCommand(ChangeSignOperation);
            EqualsOperationCommand = new RelayCommand(EqualsOperation);
            MemoryPlusOperationCommand = new RelayCommand(MemoryPlusOperation);
            MemoryMinusOperationCommand = new RelayCommand(MemoryMinusOperation);
            MemoryStoreOperationCommand = new RelayCommand(MemoryStoreOperation);
            MemoryClearOperationCommand = new RelayCommand(MemoryClearOperation);
            MemoryRecallOperationCommand = new RelayCommand(MemoryRecallOperation);
            ClearEntryOperationCommand = new RelayCommand(ClearEntryOperation);
            ClearOperationCommand = new RelayCommand(ClearOperation);
            calculatorModel = new CalculatorModel();
        }

        public float DisplayedNumber
        {
            get
            {
                return NumberUtils.ConvertToFloat(DisplayedNumberAsString);
            }
        }

        public string DisplayedNumberAsString
        {
            get
            {
                return displayedNumber;
            }
            set
            {
                displayedNumber = value;
                OnPropertyChanged();
            }
        }

        public ICommand InsertDigitOrDotCommand { get; }

        public ICommand AddOperationCommand { get; }

        public ICommand SubtractOperationCommand { get; }

        public ICommand MultiplyOperationCommand { get; }

        public ICommand DivideOperationCommand { get; }

        // TODO: Consider to rename it
        public ICommand InverseOperationCommand { get; }

        public ICommand PercentOperationCommand { get; }

        public ICommand BackspaceOperationCommand { get; }

        public ICommand SquareRootOperationCommand { get; }

        public ICommand ChangeSignOperationCommand { get; }

        public ICommand EqualsOperationCommand { get; }

        public ICommand MemoryPlusOperationCommand { get; }

        public ICommand MemoryMinusOperationCommand { get; }

        public ICommand MemoryStoreOperationCommand { get; }

        public ICommand MemoryClearOperationCommand { get; }

        public ICommand MemoryRecallOperationCommand { get; }

        public ICommand ClearEntryOperationCommand { get; }

        public ICommand ClearOperationCommand { get; }

        private void InsertDigitOrDot(string digitOrDot)
        {
            if (resetNumber)
            {
                displayedNumber = "0";
            }

            if (DisplayedNumberAsString.Length >= MaxNumberLength)
            {
                return;
            }

            if (digitOrDot == "." && DisplayedNumberAsString.Contains('.'))
            {
                return;
            }

            if (DisplayedNumberAsString == "0" && digitOrDot != ".")
            {
                DisplayedNumberAsString = digitOrDot;
            }
            else
            {
                DisplayedNumberAsString += digitOrDot;
            }

            calculatorModel.Operand = DisplayedNumber;

            resetNumber = false;
        }

        private void AddOperation()
        {
            DoArithmeticOperation(Operation.Add);
        }

        private void SubtractOperation()
        {
            DoArithmeticOperation(Operation.Subtract);
        }

        private void MultiplyOperation()
        {
            DoArithmeticOperation(Operation.Multiply);
        }

        private void DivideOperation()
        {
            DoArithmeticOperation(Operation.Divide);
        }

        private void InverseOperation()
        {
            calculatorModel.Inverse();
            DisplayedNumberAsString = calculatorModel.Operand.ToString();

            resetNumber = true;
        }

        private void PercentOperation()
        {
            calculatorModel.Percent();
            DisplayedNumberAsString = calculatorModel.Operand.ToString();
        }

        private void BackspaceOperation()
        {
            DisplayedNumberAsString = DisplayedNumberAsString.Remove(DisplayedNumberAsString.Length - 1);
            if (string.IsNullOrEmpty(DisplayedNumberAsString))
            {
                DisplayedNumberAsString = "0";
            }

            calculatorModel.Operand = DisplayedNumber;
        }

        private void SquareRootOperation()
        {
            calculatorModel.SquareRoot();
            DisplayedNumberAsString = calculatorModel.Operand.ToString();
        }

        private void ChangeSignOperation()
        {
            if (DisplayedNumberAsString.Contains('-'))
            {
                DisplayedNumberAsString = DisplayedNumberAsString.Replace("-", "");
            }
            else
            {
                DisplayedNumberAsString = "-" + DisplayedNumberAsString;
            }
        }

        private void DoArithmeticOperation(Operation operation)
        {
            // If previous operation is set, then finish it
            EqualsOperation();

            calculatorModel.Operand = DisplayedNumber;
            calculatorModel.Operation = operation;
            resetNumber = true;
        }

        private void EqualsOperation()
        {
            // TODO: Consider if it's necessary
            if (calculatorModel.Operation != Operation.None)
            {
                calculatorModel.DoOperation();
                DisplayedNumberAsString = calculatorModel.Result.ToString();
                resetNumber = true;
            }
        }

        private void MemoryPlusOperation()
        {

            calculatorModel.MemoryPlus(DisplayedNumber);
            resetNumber = true;
        }

        private void MemoryMinusOperation()
        {
            calculatorModel.MemoryMinus(DisplayedNumber);
            resetNumber = true;
        }

        private void MemoryStoreOperation()
        {
            calculatorModel.MemoryStore(DisplayedNumber);
            resetNumber = true;
        }

        private void MemoryClearOperation()
        {
            calculatorModel.MemoryClear();
            resetNumber = true;
        }

        private void MemoryRecallOperation()
        {
            DisplayedNumberAsString = calculatorModel.MemoryValue.ToString();
            resetNumber = true;
        }

        private void ClearEntryOperation()
        {
            calculatorModel.ClearEntry();
            DisplayedNumberAsString = calculatorModel.Operand.ToString();
        }

        private void ClearOperation()
        {
            calculatorModel.Clear();
            DisplayedNumberAsString = "0";
            resetNumber = true;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}