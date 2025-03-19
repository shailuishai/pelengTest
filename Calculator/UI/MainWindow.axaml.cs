using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;
using Calculator.Core.Services;
using Calculator.Core.Commands;
using Calculator.Core.Interfaces;
using Calculator.Core.Exceptions;
using System.Linq;

namespace Calculator.UI
{
    public partial class MainWindow : Window
    {
        private readonly Core.Services.Calculator _calculator;
        private readonly OperationProvider _operationProvider;
        private readonly InputParser _inputParser;
        private bool _isNewCalculation = true;
        private TextBox? _displayTextBox;

        public MainWindow()
        {
            _operationProvider = new OperationProvider();
            _inputParser = new InputParser(_operationProvider);
            _calculator = new Core.Services.Calculator(_operationProvider, _inputParser);
            
            InitializeComponent();
            
            _displayTextBox = this.FindControl<TextBox>("DisplayTextBox");
            if (_displayTextBox == null)
            {
                throw new InvalidOperationException("DisplayTextBox not found in XAML");
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && _displayTextBox != null)
            {
                if (_isNewCalculation)
                {
                    _displayTextBox.Text = button.Content?.ToString() ?? "";
                    _isNewCalculation = false;
                }
                else
                {
                    _displayTextBox.Text += button.Content?.ToString() ?? "";
                }
            }
        }

        private void OnClearClick(object sender, RoutedEventArgs e)
        {
            if (_displayTextBox != null)
            {
                _displayTextBox.Text = "0";
                _isNewCalculation = true;
            }
        }

        private void OnCalculateClick(object sender, RoutedEventArgs e)
        {
            if (_displayTextBox != null)
            {
                try
                {
                    var result = _calculator.Calculate(_displayTextBox.Text ?? "");
                    _displayTextBox.Text = result.ToString();
                    _isNewCalculation = true;
                }
                catch (Exception ex)
                {
                    _displayTextBox.Text = $"Ошибка: {ex.Message}";
                    _isNewCalculation = true;
                }
            }
        }

        private void OnAdvancedOperationsClick(object sender, RoutedEventArgs e)
        {
            if (_displayTextBox != null)
            {
                var advancedWindow = new AdvancedOperationsWindow(_operationProvider);
                advancedWindow.OperationSelected += (s, operationInfo) =>
                {
                    try
                    {
                        var (operation, arguments) = operationInfo;
                        var operationObj = _operationProvider.GetOperation(operation);
                        
                        // Парсим аргументы
                        var args = arguments.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .Select(arg => double.Parse(arg))
                            .ToArray();
                            
                        // Вызываем операцию напрямую
                        var result = operationObj.Call(args);
                        _displayTextBox.Text = result.ToString();
                        _isNewCalculation = true;
                    }
                    catch (Exception ex)
                    {
                        _displayTextBox.Text = $"Ошибка: {ex.Message}";
                        _isNewCalculation = true;
                    }
                };
                advancedWindow.ShowDialog(this);
            }
        }

        private void OnHelpClick(object sender, RoutedEventArgs e)
        {
            var helpCommand = new HelpCommand(_operationProvider);
            var helpWindow = new HelpWindow(helpCommand);
            helpWindow.ShowDialog(this);
        }
    }
} 