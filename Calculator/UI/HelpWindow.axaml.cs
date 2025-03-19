using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.IO;
using System.Text;
using Calculator.Core.Commands;

namespace Calculator.UI
{
    public partial class HelpWindow : Window
    {
        public HelpWindow(HelpCommand helpCommand)
        {
            InitializeComponent();
            
            var helpTextBlock = this.FindControl<TextBlock>("HelpTextBlock");
            
            if (helpTextBlock != null)
            {
                using (var consoleOutput = new StringWriter())
                {
                    Console.SetOut(consoleOutput);
                    helpCommand.Execute();
                    helpTextBlock.Text = consoleOutput.ToString();
                    
                    var standardOutput = new StreamWriter(Console.OpenStandardOutput())
                    {
                        AutoFlush = true
                    };
                    Console.SetOut(standardOutput);
                }
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
} 