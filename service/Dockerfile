FROM microsoft/dotnet:latest

COPY . /app
WORKDIR /app
RUN dotnet restore && dotnet build
EXPOSE 60000/tcp
ENTRYPOINT ["dotnet", "run"]