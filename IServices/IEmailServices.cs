using System;
using System.Collections.Generic;
using System.Text;

namespace IServices
{
    public interface IEmailServices
    {
        bool GetEmailContent(string userName, string pwd, string keyWords, out string content, out string rtnMsg, int tryCount = 1);
    }
}
