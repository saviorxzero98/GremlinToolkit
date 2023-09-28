using GQL.JanusGraphClients.Managements.Reports;
using GQL.JanusGraphClients.Managements.Schema;

namespace GQL.JanusGraphClients.Managements.IndexService
{
    public class GraphIndexStateService
    {
        public const int DefaultCoolingTime = 3000;
        public const int DefaultAttempts = 3;

        protected readonly JanusGraphGremlinClient _client;

        public int CoolingTime { get; set; } = DefaultCoolingTime;
        private int _attempts = DefaultAttempts;
        public int Attempts { get => _attempts; set => _attempts = (value > 0) ? value : DefaultAttempts; }

        public GraphIndexStateService(JanusGraphGremlinClient client)
        {
            _client = client;
        }

        public async Task<ManagementActionReport> RegisterIndexAsync(string indexName)
        {
            try
            {
                // 取得目前狀況
                string status = await GetIndexStatusAsync(indexName);

                // 檢查目前狀況
                if (string.IsNullOrWhiteSpace(status) || status == IndexStatus.Null)
                {
                    return ManagementActionReport.Fail();
                }

                // 判斷目前 Index 狀態
                switch (status)
                {
                    case IndexStatus.Installed:
                        return await StartRegisterIndexAsync(indexName, 0);

                    case IndexStatus.Registered:
                        return ManagementActionReport.Success();

                    case IndexStatus.Enabled:
                    case IndexStatus.Disabled:
                        return ManagementActionReport.Fail($"Error! current graph index status is \"{status}\"");
                }
                return ManagementActionReport.Fail();
            }
            catch (Exception e)
            {
                return ManagementActionReport.Fail(e);
            }
        }
        protected async Task<ManagementActionReport> StartRegisterIndexAsync(string indexName, int currentAttempts)
        {
            if (currentAttempts > Attempts)
            {
                return ManagementActionReport.Fail();
            }
            int attempts = currentAttempts + 1;

            try
            {
                var handler = new GraphIndexActionHandler(_client);
                var report = await handler.RegisterIndexAsync(indexName);

                Thread.Sleep(CoolingTime);

                // 取得目前狀況
                string status = await GetIndexStatusAsync(indexName);
                if (report.IsSuccess && status == IndexStatus.Registered)
                {
                    return report;
                }
            }
            catch
            {
            }
            return await StartRegisterIndexAsync(indexName, attempts);
        }

        public async Task<ManagementActionReport> ReindexAsync(string indexName)
        {
            try
            {
                // 取得目前狀況
                string status = await GetIndexStatusAsync(indexName);

                // 檢查目前狀況
                if (string.IsNullOrWhiteSpace(status) || status == IndexStatus.Null)
                {
                    return ManagementActionReport.Fail();
                }

                // 判斷目前 Index 狀態
                switch (status)
                {
                    case IndexStatus.Installed:
                        var registerResult = await StartRegisterIndexAsync(indexName, 0);
                        if (registerResult.IsSuccess)
                        {
                            Thread.Sleep(CoolingTime);
                            return await StartReindexAsync(indexName, 0);
                        }
                        return registerResult;

                    case IndexStatus.Registered:
                        return await StartReindexAsync(indexName, 0);

                    case IndexStatus.Enabled:
                        return ManagementActionReport.Success();

                    case IndexStatus.Disabled:
                        return ManagementActionReport.Fail($"Error! current graph index status is \"{status}\"");
                }
                return ManagementActionReport.Fail();
            }
            catch (Exception e)
            {
                return ManagementActionReport.Fail(e);
            }
        }
        protected async Task<ManagementActionReport> StartReindexAsync(string indexName, int currentAttempts)
        {
            if (currentAttempts > Attempts)
            {
                return ManagementActionReport.Fail();
            }
            int attempts = currentAttempts + 1;

            try
            {
                var handler = new GraphIndexActionHandler(_client);
                var report = await handler.ReindexAsync(indexName);

                Thread.Sleep(CoolingTime);

                // 取得目前狀況
                string status = await GetIndexStatusAsync(indexName);
                if (report.IsSuccess && status == IndexStatus.Enabled)
                {
                    return report;
                }
            }
            catch
            {
            }
            return await StartReindexAsync(indexName, attempts);
        }

        public async Task<ManagementActionReport> EnableIndexAsync(string indexName)
        {
            try
            {
                // 取得目前狀況
                string status = await GetIndexStatusAsync(indexName);

                // 檢查目前狀況
                if (string.IsNullOrWhiteSpace(status) || status == IndexStatus.Null)
                {
                    return ManagementActionReport.Fail();
                }

                // 判斷目前 Index 狀態
                switch (status)
                {
                    case IndexStatus.Installed:
                        var registerResult = await StartRegisterIndexAsync(indexName, 0);
                        if (registerResult.IsSuccess)
                        {
                            Thread.Sleep(CoolingTime);
                            return await StartEnableIndexAsync(indexName, 0);
                        }
                        return registerResult;

                    case IndexStatus.Registered:
                        return await StartEnableIndexAsync(indexName, 0);

                    case IndexStatus.Enabled:
                        return ManagementActionReport.Success();

                    case IndexStatus.Disabled:
                        return ManagementActionReport.Fail($"Error! current graph index status is \"{status}\"");
                }
                return ManagementActionReport.Fail();
            }
            catch (Exception e)
            {
                return ManagementActionReport.Fail(e);
            }
        }
        protected async Task<ManagementActionReport> StartEnableIndexAsync(string indexName, int currentAttempts)
        {
            if (currentAttempts > Attempts)
            {
                return ManagementActionReport.Fail();
            }
            int attempts = currentAttempts + 1;

            try
            {
                var handler = new GraphIndexActionHandler(_client);
                var report = await handler.EnableIndexAsync(indexName);

                Thread.Sleep(CoolingTime);

                // 取得目前狀況
                string status = await GetIndexStatusAsync(indexName);
                if (report.IsSuccess && status == IndexStatus.Enabled)
                {
                    return report;
                }
            }
            catch
            {

            }
            return await StartEnableIndexAsync(indexName, attempts);
        }

        public async Task<ManagementActionReport> DisableIndexAsync(string indexName)
        {
            try
            {
                // 取得目前狀況
                string status = await GetIndexStatusAsync(indexName);

                // 檢查目前狀況
                if (string.IsNullOrWhiteSpace(status) || status == IndexStatus.Null)
                {
                    return ManagementActionReport.Fail();
                }

                // 判斷目前 Index 狀態
                switch (status)
                {
                    case IndexStatus.Disabled:
                        return ManagementActionReport.Success();

                    case IndexStatus.Installed:
                    case IndexStatus.Registered:
                    case IndexStatus.Enabled:
                        return await StartDisableIndexAsync(indexName, 0);
                }
                return ManagementActionReport.Fail();
            }
            catch (Exception e)
            {
                return ManagementActionReport.Fail(e);
            }
        }
        protected async Task<ManagementActionReport> StartDisableIndexAsync(string indexName, int currentAttempts)
        {
            if (currentAttempts > Attempts)
            {
                return ManagementActionReport.Fail();
            }
            int attempts = currentAttempts + 1;

            try
            {
                var handler = new GraphIndexActionHandler(_client);
                var report = await handler.DisableIndexAsync(indexName);

                Thread.Sleep(CoolingTime);

                // 取得目前狀況
                string status = await GetIndexStatusAsync(indexName);
                if (report.IsSuccess || status == IndexStatus.Disabled)
                {
                    return report;
                }
            }
            catch
            {
            }
            return await StartDisableIndexAsync(indexName, attempts);
        }

        public async Task<ManagementActionReport> RemoveIndexAsync(string indexName)
        {
            try
            {
                // 取得目前狀況
                string status = await GetIndexStatusAsync(indexName);

                // 檢查目前狀況
                if (string.IsNullOrWhiteSpace(status) || status == IndexStatus.Null)
                {
                    return ManagementActionReport.Fail();
                }

                // 判斷目前 Index 狀態
                switch (status)
                {
                    case IndexStatus.Disabled:
                        var handler = new GraphIndexActionHandler(_client);
                        return await handler.DisableIndexAsync(indexName);

                    case IndexStatus.Installed:
                    case IndexStatus.Registered:
                    case IndexStatus.Enabled:
                        return await StartDisableIndexAsync(indexName, 0);
                }
                return ManagementActionReport.Fail();
            }
            catch (Exception e)
            {
                return ManagementActionReport.Fail(e);
            }
        }


        protected async Task<string> GetIndexStatusAsync(string indexName)
        {
            var handler = new GraphIndexActionHandler(_client);

            // 取得目前 Index 狀態
            var currentStatus = await handler.GetStatusAsync(indexName);
            if (!currentStatus.IsSuccess ||
                currentStatus.Statuses == null ||
                !currentStatus.Statuses.Any(s => !string.IsNullOrWhiteSpace(s)))
            {
                return IndexStatus.Null;
            }

            // 判斷目前 Index 狀態
            string state = currentStatus.Statuses.FirstOrDefault(s => !string.IsNullOrWhiteSpace(s)).ToUpper();

            switch (state)
            {
                case IndexStatus.Installed:
                case IndexStatus.Registered:
                case IndexStatus.Enabled:
                case IndexStatus.Disabled:
                    return state;
            }

            return IndexStatus.Null;
        }

    }
}
