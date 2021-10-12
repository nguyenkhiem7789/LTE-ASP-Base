using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Cache
{
    public class RedisConnection
    {
        private readonly string _serverIp;
        private readonly string _password;
        private const int IoTimeOut = 5000;
        private const int SyncTimeout = 5000;
        private ConnectionMultiplexer[] _writeConnections;
        private ConnectionMultiplexer[] _readConnections;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        public static readonly object SyncReadConnectionLock = new object();
        public static readonly object SyncWriteConnectionLock = new object();
        public static int _readIndexConnection = 0;
        public static int _writeIndexConnection = 0;

        private async Task<ConnectionMultiplexer> CreateConnection(string ip)
        {
            var config = ConfigurationOptions.Parse(ip);
            config.KeepAlive = 180;
            config.SyncTimeout = SyncTimeout;
            config.AbortOnConnectFail = true;
            config.AllowAdmin = true;
            config.ConnectTimeout = IoTimeOut;
            config.ConnectRetry = 5;
            config.Password = _password;
            var connection = await ConnectionMultiplexer.ConnectAsync(config);
            return connection;
        }

        public async Task MakeConnection()
        {
            await _semaphore.WaitAsync();
            var connections = await CreateConnection(_serverIp);
            var endpoint = _serverIp.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                List<ConnectionMultiplexer> connectionMasters = new List<ConnectionMultiplexer>();
                List<ConnectionMultiplexer> connectionSlaves = new List<ConnectionMultiplexer>();
                foreach (var endPoint in endpoint)
                {
                    var server = connections.GetServer(endPoint);
                    if(server == null)
                        continue;
                    ConnectionMultiplexer connectionMultiplexer = await CreateConnection(endPoint);
                    if (server.IsConnected)
                        connectionSlaves.Add(connectionMultiplexer);
                    if(!server.IsReplica)
                        connectionMasters.Add(connectionMultiplexer);
                }

                _writeConnections = connectionMasters.ToArray();
                _readConnections = connectionSlaves.ToArray();
                foreach (var writeConnection in _writeConnections)
                {
                    writeConnection.ConnectionFailed += async (sender, args) => { await MakeConnection(); };
                }

                foreach (var readConnection in _readConnections)
                {
                    readConnection.ConnectionFailed += async (sender, args) => { await MakeConnection(); };
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                await connections.CloseAsync(true);
                _semaphore.Release();
            }
        }

        public RedisConnection(string serverIp, string password)
        {
            _serverIp = serverIp;
            _password = password;
        }

        public IDatabase GetWriteConnection()
        {
            lock (SyncWriteConnectionLock)
            {
                if (_writeIndexConnection >= _writeConnections.Length)
                {
                    _writeIndexConnection = 0;
                }

                var connection = _writeConnections[_writeIndexConnection++];
                var db = connection.GetDatabase();
                return db;
            }
        }

        public IDatabase GetReadConnection()
        {
            lock (SyncWriteConnectionLock)
            {
                if (_readIndexConnection >= _readConnections.Length)
                {
                    _readIndexConnection = 0;
                }

                var connection = _readConnections[_readIndexConnection++];
                var db = connection.GetDatabase();
                return db;
            }
        }
    }
}