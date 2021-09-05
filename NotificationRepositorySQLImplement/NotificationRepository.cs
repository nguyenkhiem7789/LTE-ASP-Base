using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BaseRepositories;
using Dapper;
using NotificationCommands.Queries;
using NotificationDomains;
using NotificationReadModels;
using NotificationRepository;

namespace NotificationRepositorySQLImplement
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public NotificationRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public async Task<RNotification[]> Gets(NotificationGetsQuery query)
        {
            return await _dbConnectionFactory.WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Keyword", query.keyword);
                var data = await connection.QueryAsync<RNotification>("[Notification_Gets]", parameters,
                    commandType: CommandType.StoredProcedure);
                var users = data.ToArray();
                return users;
            });
        }

        public async Task<RNotification> GetById(NotificationGetByIdQuery query)
        {
            return await _dbConnectionFactory.WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", query.Id);
                var data = await connection.QueryFirstOrDefaultAsync<RNotification>("[Notification_GetById]",
                    parameters, commandType: CommandType.StoredProcedure);
                return data;
            });
        }

        public async Task Add(Notification notification)
        {
            await _dbConnectionFactory.WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", notification.Id);
                parameters.Add("@Title", notification.Title);
                parameters.Add("@Content", notification.Content);
                parameters.Add("@CreatedDate", notification.CreatedDate);
                parameters.Add("@CreatedDateUtc", notification.CreateDateUtc);
                parameters.Add("@Code", notification.Code);
                parameters.Add("@Status", notification.Status);
                var data = connection.Execute("[Notification_Insert]", parameters,
                    commandType: CommandType.StoredProcedure);
                return await Task.FromResult(true);
            });

        }

        public async Task Change(Notification notification)
        {
            await _dbConnectionFactory.WithConnection(async (connection) =>
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ID", notification.Id);
                parameters.Add("@Title", notification.Title);
                parameters.Add("@Content", notification.Content);
                parameters.Add("@Status", notification.Status);
                var data = connection.Execute("[Notification_Update]", parameters,
                    commandType: CommandType.StoredProcedure);
                return await Task.FromResult(true);
            });
        }
    }
}