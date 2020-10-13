using IServices;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Services
{
    public class EmailServices : IEmailServices
    {
        public bool GetEmailContent(string userName, string pwd, string keyWords, out string content, out string rtnMsg, int tryCount = 1)
        {
            content = "";
            rtnMsg = "";
            try
            {
                using (var client = new ImapClient())
                {
                    if (userName.Contains("@163.com"))
                    {
                        client.Connect("imap.163.com", 993, true);
                    }
                    else if (userName.Contains("@126.com"))
                    {
                            
                    }

                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    try
                    {
                        client.Authenticate(userName, pwd);
                    }
                    catch (AuthenticationException ex)
                    {
                        throw new Exception("无效的用户名或密码：" + ex.Message);                     
                    }
                    catch (ImapCommandException ex)
                    { 
                        throw new Exception("尝试验证错误：" + ex.Message);
                    }
                    catch (ImapProtocolException ex)
                    { 
                        throw new Exception("尝试验证时的协议错误：" + ex.Message);
                    }
                    catch (Exception ex)
                    {                     
                        throw new Exception("账户认证错误：" + ex.Message);
                    }

                    var clientImplementation = new ImapImplementation
                    {
                        Name = "我的邮箱",
                        Version = "2.0"
                    };
                    var serverImplementation = client.Identify(clientImplementation);

                    int count = 0;
                methodStart:
                    count++;
                    if (count <= tryCount)
                    {
                        //获取未读邮件
                        var inbox = client.Inbox;
                        inbox.Open(FolderAccess.ReadWrite);
                        var uidss = inbox.Search(SearchQuery.NotSeen);
                        if (uidss == null || uidss.Count == 0)
                        {
                            //没有未读邮件，
                            if (count < tryCount)
                            {
                                Thread.Sleep(1000);
                            }
                            goto methodStart;
                        }

                        for (int i = 0; i < uidss.Count; i++)
                        {
                            var message = inbox.GetMessage(uidss[i]);
                            string bodyText = message.HtmlBody;
                            if (bodyText.Contains(keyWords))
                            {
                                content = bodyText;

                                Regex regex = new Regex(@"<[^>]+>|</[^>]+>");

                                string stroutput = regex.Replace(bodyText, "");
                                stroutput = stroutput.Replace(" ", "");
                                stroutput = stroutput.Replace("\r\n", "");
                                stroutput = stroutput.Replace("\r", "");
                                stroutput = stroutput.Replace("\n", "");

                                content = stroutput;
                                if (content.Contains("验证码是："))
                                {
                                    content = content.Substring(content.IndexOf("验证码是：") + 5, content.IndexOf("验证码有效期") - (content.IndexOf("验证码是：") + 5));
                                }


                                inbox.SetFlags(uidss[i], MessageFlags.Seen, false);
                                break;
                            }
                        }

                        //没有获取到指定的邮件
                        if (string.IsNullOrEmpty(content))
                        {
                            Thread.Sleep(1000);
                            goto methodStart;
                        }                        
                    }
                    else
                    {
                        rtnMsg = "未获取到符合条件的邮件！";
                    }                   
                    client.Disconnect(true);
                } 

            }
            catch (Exception ex)
            {
                rtnMsg = ex.Message;
            }
           return !string.IsNullOrEmpty(content);

        }


    }
}
