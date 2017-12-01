FROM microsoft/dotnet:2.0.0-sdk-2.0.2-jessie AS installer-env

ENV PublishWithAspNetCoreTargetManifest false

COPY . /runtime
WORKDIR /runtime

RUN dotnet build WebJobs.Script.sln
RUN dotnet publish src/WebJobs.Script.WebHost/WebJobs.Script.WebHost.csproj --output /azure-functions-runtime

# Runtime image
FROM microsoft/dotnet:2.0.0-runtime-jessie

COPY --from=installer-env ["/azure-functions-runtime", "/azure-functions-runtime"]

ENV AzureWebJobsScriptRoot=/app

CMD ["dotnet", "/azure-functions-runtime/Microsoft.Azure.WebJobs.Script.WebHost.dll"]
