FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

COPY ./BugReportModule.csproj ./BugReportModule.csproj
RUN dotnet add package Microsoft.EntityFrameworkCore
RUN dotnet add package Microsoft.EntityFrameworkCore.Design
RUN dotnet add package Microsoft.EntityFrameworkCore.Tools
RUN dotnet add package Microsoft.EntityFrameworkCore.InMemory
RUN dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
RUN dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL.Design
RUN dotnet add package System.IdentityModel.Tokens.Jwt
RUN dotnet add package Microsoft.IdentityModel.Tokens
RUN dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

COPY . ./
RUN dotnet restore

CMD [ "dotnet", "run", "--urls", "https://0.0.0.0:5001" ]
