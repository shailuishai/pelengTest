using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using Calculator.Core.Interfaces;
using Calculator.Core.Services;
using Calculator.Core.Exceptions;

namespace Calculator.UI
{
    public class OperationViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Usage { get; set; }
        public string Parameters { get; set; }
        public IOperation Operation { get; set; }
        
        public OperationViewModel(IOperation operation, string name)
        {
            Name = name;
            Description = operation.Description;
            Usage = operation.Usage;
            Parameters = operation.Parameters;
            Operation = operation;
        }
    }
    
    public partial class AdvancedOperationsWindow : Window
    {
        public event EventHandler<(string operation, string arguments)>? OperationSelected;

        private readonly OperationProvider _operationProvider;
        private TextBox? _argumentsTextBox;
        private ListBox? _operationsListBox;

        // Список базовых операций, которые не должны отображаться в дополнительном окне
        private readonly string[] _basicOperations = new[] 
        { 
            "add", "subtract", "multiply", "divide", 
            "basicadd", "basicsubtract", "basicmultiply", "basicdivide"
        };

        public AdvancedOperationsWindow(OperationProvider operationProvider)
        {
            _operationProvider = operationProvider;
            InitializeComponent();
            
            _operationsListBox = this.FindControl<ListBox>("OperationsListBox");
            _argumentsTextBox = this.FindControl<TextBox>("ArgumentsTextBox");
            
            if (_operationsListBox != null)
            {
                var operations = _operationProvider.GetOperations()
                    .Where(op => !_basicOperations.Contains(op.GetType().Name.ToLower().Replace("operation", "")))
                    .Select(op => new OperationViewModel(
                        op, 
                        op.GetType().Name.ToLower().Replace("operation", "")
                    ))
                    .OrderBy(op => op.Name)
                    .ToList();
                    
                _operationsListBox.ItemsSource = operations;
                _operationsListBox.SelectionChanged += OnOperationSelected;
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnOperationSelected(object? sender, SelectionChangedEventArgs e)
        {
            if (_operationsListBox?.SelectedItem is OperationViewModel operation && _argumentsTextBox != null)
            {
                _argumentsTextBox.Watermark = $"Введите аргументы через пробел (например: {operation.Usage})";
            }
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnExecuteClick(object sender, RoutedEventArgs e)
        {
            if (_operationsListBox?.SelectedItem is OperationViewModel operation && _argumentsTextBox != null)
            {
                var arguments = _argumentsTextBox.Text?.Trim() ?? "";
                // Форматируем аргументы, сохраняя отрицательные числа
                arguments = arguments.Replace("  ", " ").Replace(" -", " -");
                OperationSelected?.Invoke(this, (operation.Name, arguments));
                Close();
            }
        }
    }
} 