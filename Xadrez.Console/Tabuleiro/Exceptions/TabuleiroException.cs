namespace Xadrez.ConsoleApp.Tabuleiro.Exceptions
{
    internal class TabuleiroException : ApplicationException
    {
        public TabuleiroException(string message, Exception innerExeption = null) : base(message, innerExeption) { }
    }
}
