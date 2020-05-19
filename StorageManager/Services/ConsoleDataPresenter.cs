using StorageManager.Interfaces;
using System;

namespace StorageManager.Services
{
    public class ConsoleDataPresenter : IDataPresenter
    {
        public void PrintFormattedText(string text)
        {
            Console.Write(text);
        }
    }
}