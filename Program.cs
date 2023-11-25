using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace NeonConsole
{
    class NeonConsole{
        public static void Main(string[] args){

            if(args[0].ToLower() == "neon"){
                // OpenNeonDesktopReg();
                CloseWindows();
                OpenNeonDesktop();
            }
            else if(args[0].ToLower() == "windows"){
                // OpenWindowsReg();
                CloseNeonDesktop();
                OpenWindows();
            }
            
            // Console.WriteLine("Press any key to exit ......");
            // Console.ReadLine();
        }

        static void OpenNeonDesktop(){
            string NeonExeName = "neonai.exe";
            var assemblyLocation = Assembly.GetEntryAssembly()?.Location;
            var currentPath = Path.GetDirectoryName(assemblyLocation);
            var finalPath = Path.GetFullPath(Path.Combine(currentPath != null? currentPath : "", @"..\..\.."));
            // var currentPath = "C:\\Users\\Aileene\\AppData\\Local\\Programs\\neonai";

            var finalExecutable = $"{finalPath}\\{NeonExeName}";

            Process process = new Process(){
                StartInfo = new ProcessStartInfo(finalExecutable){
                    WorkingDirectory = Path.GetDirectoryName(finalExecutable)
                }
            };

            process.Start();
            
            

            Console.WriteLine("Running Neon Desktop");
        }

        static void OpenWindows(){
            string NeonExeName = "explorer.exe";
            var currentPath = "C:\\Windows";

            var finalExecutable = $"{currentPath}\\{NeonExeName}";

            Console.WriteLine("Running Windows");

            Process.Start(finalExecutable);
        }

        static void CloseNeonDesktop(){
            var neonProcesses = Process.GetProcessesByName("neonai");
            foreach(var process in neonProcesses){
                process.Kill();
            }
        }

        static void CloseWindows(){
            RegistryKey ourKey = Registry.LocalMachine;
            ourKey = ourKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true)!;
            ourKey.SetValue("AutoRestartShell", 0);
            // Kill the explorer by the way you've post and do your other work
            // ourKey.SetValue("AutoRestartShell", 1);
            var windowsProcesses = Process.GetProcessesByName("explorer");
            foreach(var process in windowsProcesses){
                process.Kill();
            }
        }

        // static void OpenNeonDesktopReg(){
        //     string NeonExeName = "neonai.exe";
        //     var assemblyLocation = Assembly.GetEntryAssembly()?.Location;
        //     var currentPath = Path.GetDirectoryName(assemblyLocation);

        //     var finalExecutable = $"{currentPath}\\{NeonExeName}";

        //     RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);

        //     RegistryKey regKey = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true)!;
        //     regKey?.SetValue("Shell", finalExecutable, RegistryValueKind.String);
        //     regKey?.Close();

        //     Console.ReadLine();
        // }

        // static void OpenWindowsReg(){
        //     RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);

        //     RegistryKey regKey = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true)!;
        //     regKey?.SetValue("Shell", "explorer.exe", RegistryValueKind.String);
        //     regKey?.Close();

        //     Console.ReadLine();
        // }
    }
}