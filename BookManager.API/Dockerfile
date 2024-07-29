ARG BASE_IMAGE=mcr.microsoft.com/dotnet/aspnet:6.0
FROM $BASE_IMAGE

WORKDIR /app

RUN adduser --disabled-password --gecos "" app -u 1000
COPY --chown=app:app app/Super.EWalletCore.Integration.Service.Api/. .

USER 1000

ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Super.EWalletCore.Integration.Service.Api.dll"]
