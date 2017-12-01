using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Azure.WebJobs.Script.Abstractions;
using Microsoft.Azure.WebJobs.Script.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Microsoft.Azure.WebJobs.Script.Rpc
{
    internal class GolangWorkerProvider : IWorkerProvider
    {
        public WorkerDescription GetDescription() => new WorkerDescription
        {
            Language = "Go",
            Extension = ".go",
            DefaultExecutablePath = "/azure-functions-runtime/workers/go/golang-worker"
        };

        public bool TryConfigureArguments(ArgumentsDescription args, IConfiguration config, ILogger logger)
        {
            return true;
        }
    }
}