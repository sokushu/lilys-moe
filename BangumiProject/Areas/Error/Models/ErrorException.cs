using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Error.Models
{
    public class ErrorException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public ErrorException() : base() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message"></param>
        public ErrorException(string Message) : base(Message)
        {
            Messages = Message;
            Change = true;
            Mes = true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StatesCode"></param>
        public ErrorException(int StatesCode)
        {
            this.StatesCode = StatesCode;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StatesCode"></param>
        /// <param name="Message"></param>
        public ErrorException(int StatesCode, string Message) : base(Message)
        {
            this.StatesCode = StatesCode;
            Messages = Message;
            Change = true;
            Mes = true;
        }
        /// <summary>
        /// 
        /// </summary>
        public int StatesCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private bool Mes = false;
        private bool Change = false;
        /// <summary>
        /// 
        /// </summary>
        private string Messages { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public override string Message
        {
            get
            {
                if (Change == true)
                {
                    return Messages;
                }
                else
                {
                    return base.Message ?? Messages;
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message"></param>
        public void SetMessage(string Message)
        {
            if (Change == false)
            {
                Messages = Message;
                Change = true;
            }
            else
            {
                if (!Mes)
                {
                    Messages = Message;
                }
            }
        }
    }
}
