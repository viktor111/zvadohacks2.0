using Euroins.Payment.Data.Repositories;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using ZvadoHacks.Data.Entities;
using ZvadoHacks.Infrastructure.Email;
using ZvadoHacks.Models.PortScanner;
using ZvadoHacks.Models.Types;

namespace ZvadoHacks.Services
{
    public class PortScannerService : IPortScannerService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PortScannerService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<PortScanResponse> Scan(PortScanRequest request, string userId)
        {
            try
            {
                var scope = _serviceScopeFactory.CreateScope();
                List<Task<Port>> scanTasks = new List<Task<Port>>();
                List<int> portsToScan = new List<int>();

                switch (request.PortScanType)
                {
                    case PortScanType.Base:
                        portsToScan = Enumerable.Range(1, 1024).ToList();
                        break;
                    case PortScanType.Medium:
                        portsToScan = Enumerable.Range(1, 49151).ToList();
                        break;
                    case PortScanType.Aggressive:
                        portsToScan = Enumerable.Range(1, 65535).ToList();
                        break;
                    case PortScanType.Random:
                        Random random = new Random();
                        portsToScan = Enumerable.Range(1, 65535).OrderBy(_ => random.Next()).Take(100).ToList();
                        break;
                    default:
                        portsToScan = Enumerable.Range(1, 1024).ToList();
                        break;
                }

                var hosts = GetIpAddresses(request.Host);

                foreach (int port in portsToScan)
                {
                    foreach (var host in hosts)
                    {
                        scanTasks.Add(ScanPort(host, port));
                    }
                }

                var st = new Stopwatch();

                st.Start();
                Port[] openPorts = await Task.WhenAll(scanTasks);
                st.Stop();

                PortScanResponse response = new PortScanResponse
                {
                    PortScanType = request.PortScanType,
                    OpenPorts = openPorts.Count(p => p.IsOpen),
                    Ports = openPorts.Where(p => p.IsOpen).ToList(),
                    TimeTaken = st.Elapsed.ToString(),
                    IpsScanned = hosts
                };
                var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();
                var scanRepository = scope.ServiceProvider.GetRequiredService<IRepository<Scan>>();

                await scanRepository.Add(new Scan()
                {
                    UserId = userId,
                    PortScanResponse = response,
                });

                var user = await userRepository.FindOne(x => x.Id == userId);

                // ToDo: Fix email sending
                // await EmailClient.Send(user.Email, "Zvado Port Scan", "The port scan was compleated check back on your scans history!");

                scope.Dispose();

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<Port> ScanPort(string host, int port)
        {
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                try
                {
                    await socket.ConnectAsync(host, port);
                    return new Port { Number = port, IsOpen = true, Host = host };
                }
                catch (SocketException)
                {
                    return new Port { Number = port, IsOpen = false, Host = host };
                }
            }
        }

        private List<string> GetIpAddresses(string domain)
        {
            IPAddress[] ipAddresses = Dns.GetHostAddresses(domain);

            var addresses = new List<string>();

            foreach (IPAddress ipAddress in ipAddresses)
            {
                addresses.Add(ipAddress.ToString());
            }

            return addresses;
        }
    }
}
