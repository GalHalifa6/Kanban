using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    ///<summary>Class <c>Response</c> represents the result of a call to a void function. 
    ///If an exception was thrown, <c>ErrorOccured = true</c> and <c>ErrorMessage != null</c>. 
    ///Otherwise, <c>ErrorOccured = false</c> and <c>ErrorMessage = null</c>.</summary>
    public class Response<T>
    {
        public readonly string ErrorMessage;
        public bool ErrorOccured { get => ErrorMessage != null; }
        public readonly T Result;
        internal Response() { }
        internal Response(string msg)
        {
            this.ErrorMessage = "{ErrorMessage: " + msg + ", ReturnValue: null}";
        }

        internal Response(T val, bool success)
        {
            Result = val;
        }
    }
}
