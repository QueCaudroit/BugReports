FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /source

COPY . ./
RUN dotnet add package Microsoft.EntityFrameworkCore
RUN dotnet add package Microsoft.EntityFrameworkCore.Design
RUN dotnet add package Microsoft.EntityFrameworkCore.Tools
RUN dotnet add package Microsoft.EntityFrameworkCore.InMemory
RUN dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
RUN dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL.Design
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet restore

CMD [ "dotnet", "run", "--urls", "https://0.0.0.0:5001" ]
