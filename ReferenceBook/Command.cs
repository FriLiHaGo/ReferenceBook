using System;
using System.Windows.Input;

namespace ReferenceBook
{
    /// <summary>
    /// Простая команда
    /// </summary>
    public class Command : ICommand
    {
        private Action<object> onExecute;
        private Func<object, bool> canExecute;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="onExecute">Действие при выполнении команды</param>
        /// <param name="canExecute">Вызывается для проверки возможности выполнения</param>
        public Command(Action<object> onExecute, Func<object, bool> canExecute = null)
        {
            this.onExecute = onExecute;
            this.canExecute = canExecute;
        }

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Проверить возможность выполнения
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return canExecute != null ? canExecute.Invoke(parameter) : true;
        }

        /// <summary>
        /// Выполнить
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            onExecute?.Invoke(parameter);
        }
    }
}
