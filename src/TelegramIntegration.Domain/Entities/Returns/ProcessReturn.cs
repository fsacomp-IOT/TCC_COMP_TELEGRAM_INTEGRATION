using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramIntegration.Domain.Entities.Returns
{
    public class ProcessReturn
    {
        public int QtdSuccess { get; set; }
        public int QtdFail { get; set; }
    }
}
