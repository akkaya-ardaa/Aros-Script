using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Try
{
    public static class ErrorHandler
    {
        public static void Handle(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
