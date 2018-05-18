using CheckEnd.Frms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckEnd
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Threading.Mutex mutex;
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            string applicationPath = Application.StartupPath;
            if (Regex.IsMatch(applicationPath, "(c|C):"))//程序在C盘
            {
                //判断当前登录用户是否为管理员
                if (principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator))
                { 
                    //如果是管理员，则直接运行 
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    mutex = new System.Threading.Mutex(true, "OnlyRun");
                    if (mutex.WaitOne(0, false))
                    {
                        Application.Run(new Frm_Login());
                    }
                    else
                    {
                        MessageBox.Show("程序已运行");
                        System.Environment.Exit(0);
                    }

                }
                else
                {
                    //创建启动对象
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.UseShellExecute = true;
                    startInfo.WorkingDirectory = Environment.CurrentDirectory;
                    startInfo.FileName = Application.ExecutablePath;
                    //设置启动动作,确保以管理员身份运行
                    startInfo.Verb = "runas";
                    try
                    {
                        System.Diagnostics.Process.Start(startInfo);
                    }
                    catch
                    {
                        return;
                    }
                    //退出
                    Application.Exit();
                }
                return;
            }
            else
            {
                 
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                mutex = new System.Threading.Mutex(true, "OnlyRun");
                if (mutex.WaitOne(0, false))
                { 
                    Application.Run(new Frm_Login()); 
                }
                else
                { 
                    MessageBox.Show("程序已运行");
                    System.Environment.Exit(0);
                }

            }
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            ////Application.Run(new EndCheckFrm());
            //Application.Run(new Frm_Login());
        }
       
    }
}
